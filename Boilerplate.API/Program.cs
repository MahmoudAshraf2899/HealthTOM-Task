using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Boilerplate.API;
using Boilerplate.API.Middlewares;
using Boilerplate.API.ServiceBuilderExtensions;
using Boilerplate.Application;
using Boilerplate.Application.Helpers;
using Boilerplate.Contracts;
using Boilerplate.Contracts.Helpers;
using Boilerplate.Core;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Infrastructure;
using Boilerplate.Infrastructure.Extentions;
using Boilerplate.Infrastructure.TokenProviders;
using Boilerplate.Shared;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Helpers;
using DinkToPdf;
using DinkToPdf.Contracts;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//#region Hangfire

builder.Services.RegisterHangFire(builder.Configuration.GetConnectionString("BoilerplateConnectionString"));

//#endregion

// allow cors for SignalR

#region Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
        builder.WithHeaders(HeaderNames.ContentType, "x-custom-header")
            .WithHeaders(HeaderNames.Accept, "*/*")
            .WithHeaders(HeaderNames.AcceptEncoding, "gzip, deflate, br")
            .WithHeaders(HeaderNames.Connection, "keep-alive").AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            .SetIsOriginAllowed((hosts) => true));
});

#endregion

#region Serilog
ConfigureLogs();
builder.Host.UseSerilog();
void ConfigureLogs()
{
    // Get the environment which the application is running on
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    // Get the configuration 
    var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

    // Create Logger
    Log.Logger = new LoggerConfiguration()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentUserName()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
        .Enrich.WithExceptionDetails() // Adds details exception
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureELS(configuration))
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{builder.Configuration["ApplicationName"]}-logs-{builder.Environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
    };
}
#endregion



builder.Services.AddControllers(configur =>
{
    configur.ReturnHttpNotAcceptable = true;

    configur.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status200OK));

    configur.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status400BadRequest));

    configur.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status404NotFound));

    configur.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status500InternalServerError));
});
builder.Services.AddHttpContextAccessor();

#region Json newtonsoft and json patch

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddControllersWithViews(options =>
{
    options.InputFormatters.Insert(0, new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value
        .InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>().First());
});

#endregion

#region Connection String

string connectionString = builder.Configuration.GetConnectionString("BoilerplateConnectionString");
builder.Services.AddDbContext(connectionString);

#endregion

#region HttpContextAccessor

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

#endregion

#region Add Identity

builder.Services.AddIdentity();

builder.Services.Configure<ResourcesPath>(builder.Configuration.GetSection("ResourcesPath"));

//builder.Services.AddSingleton(emailConfig);
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

// email confirmation
builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromDays(3));
// password confirmation
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
});

#endregion

#region JWT Auth

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = JWTKeys.Issuer,
            ValidAudience = JWTKeys.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKeys.Key)),
            ClockSkew = TimeSpan.FromMinutes(30)
        };
    });

#endregion

#region autoMapper

builder.Services.AddAutoMapper(typeof(ApplicationModule));

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile(provider.GetService<IServer>(), provider.GetService<IUnitOfWork>()));
}).CreateMapper());
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

#endregion

#region localization and globalization

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddLocalization();

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulturesInfo = Culture.GetSupportedCulturesInfo();
    options.DefaultRequestCulture = new RequestCulture(supportedCulturesInfo[0], supportedCulturesInfo[0]);
    options.SupportedCultures = supportedCulturesInfo;
    options.SupportedUICultures = supportedCulturesInfo;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    };
});

#endregion

#region autoFac Configure IOC Container for other Projects

//var isDevelopment = builder.Environment.IsDevelopment();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new SharedModule());
    builder.RegisterModule(new ContractsModule());
    builder.RegisterModule(new CoreModule());
    builder.RegisterModule(new InfrastructureModule());
    builder.RegisterModule(new ApplicationModule());
});

#endregion

#region API .Net Core IOC Container
builder.Services.AddTransient<HolderOfDTO>();

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region SignalR

builder.Services.AddSignalR();

#endregion

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Boilerplate API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    c.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
// Configure the HTTP request pipeline.

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Local");
    options.SwaggerEndpoint(url: "./swagger/v1/swagger.json", name: "IIS");
});

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseMiddleware<TokenEncryptionMiddleware>();
//app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("CORSPolicy");
app.UseStaticFiles();
app.RegisterHangFireDashBoard();

app.UseAuthentication();

app.UseAuthorization();
app.UseMiddleware<LogUserNameMiddleware>();
app.UseHangfireDashboard();
app.MapControllers();

#region SignalR

app.MapHub<DataHub>("/dataHub");

#endregion

app.Run();