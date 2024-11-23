using Boilerplate.Core.Entities.Auth;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.TokenProviders;
using Boilerplate.Shared.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Infrastructure.Extentions
{
    public static class ServiceCollectionExtention
    {
        // extention for bulider.service
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<BoilerplateDBContext>(options =>
            {
                options.UseSqlServer(connectionString, b =>
                {
                    b.MigrationsAssembly(typeof(BoilerplateDBContext).Assembly.FullName);
                    // .UseNetTopologySuite();
                    b.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                });
            });

        public static IdentityBuilder AddIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<User, IdentityRole>(option =>
            {
                //option.Password.RequiredLength = 7;
                //option.Password.RequireDigit = false;
                //option.Password.RequireUppercase = false;
                option.SignIn.RequireConfirmedAccount = true;
                option.SignIn.RequireConfirmedEmail = true;
                option.User.RequireUniqueEmail = true;
                option.Tokens.EmailConfirmationTokenProvider = Res.emailConfirmation;
            }).AddEntityFrameworkStores<BoilerplateDBContext>()
              .AddDefaultTokenProviders()
              .AddTokenProvider<EmailConfirmationTokenProvider<User>>(Res.emailConfirmation); ;
        }
    }
}
