using Boilerplate.Shared.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Boilerplate.Infrastructure.Extentions
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
                // await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var errorDetails = new ErrorDetails();
            errorDetails.Success = false;
            errorDetails.Message = Res.ErrorMessage;
            errorDetails.Error = Res.ErrorMessage;

            switch (exception)
            {
                case AppException e:
                    //errorDetails.Errors = e.Errors;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;//400
                    //foreach (var item in e.Errors)
                    //{
                    //    errorDetails.Error += item.Value + "\n";
                    //}
                    break;
                case DbUpdateConcurrencyException e:
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;//422
                    break;
                case DbUpdateException e:
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;//422
                    //errorDetails.Error = e.InnerException.Message;
                    errorDetails.Message = "Error while saving data to database, please try again later!";
                    break;
                case BadHttpRequestException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;//400
                    break;
                case KeyNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;//404
                    break;
                default:
                    //errorDetails.Exception = exception.ToString();
                    errorDetails.Message = "Internal server error, please try again later!";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500
                    break;
            }

            errorDetails.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsJsonAsync(errorDetails);

            throw exception;
        }
    }
}
