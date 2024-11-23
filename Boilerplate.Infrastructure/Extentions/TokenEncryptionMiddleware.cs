using Boilerplate.Contracts.IServices.Services.EncryptionAndDecryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Boilerplate.Infrastructure.Extentions
{
    public class TokenEncryptionMiddleware
    {
        #region DI

        private RequestDelegate Next { get; }
        private ILogger<TokenEncryptionMiddleware> Logger { get; }
        public IEncryptionAndDecryptionService _encryptionAndDecryptionService { get; }

        public TokenEncryptionMiddleware(
            RequestDelegate next,
            ILogger<TokenEncryptionMiddleware> logger,
            IEncryptionAndDecryptionService encryptionAndDecryptionService)
        {
            Logger = logger;
            Next = next;
            _encryptionAndDecryptionService = encryptionAndDecryptionService;
        }

        #endregion


        #region InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                vDecryptAuthorizationHeaderValue(httpContext);
                await Next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void vDecryptAuthorizationHeaderValue(HttpContext httpContext)
        {
            try
            {
                string AccessToken = httpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    var token = AccessToken.Replace("Bearer ", "").Replace("bearer ", "");
                    string sDecryptedAuthorizationHeaderValue = _encryptionAndDecryptionService.DecryptData(token);

                    httpContext.Request.Headers["Authorization"] = $"Bearer {sDecryptedAuthorizationHeaderValue}";
                }
            }
            catch (Exception oException)
            {
                Logger.LogError($"Encryption Middleware Exception: {oException}");
            }
        }
        protected bool ValidToken(HttpContext httpContext)
        {
            bool valid = false;
            try
            {
                string AccessToken = httpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    var tok = AccessToken.Replace("Bearer ", "");
                    var jwttoken = new JwtSecurityTokenHandler().ReadToken(tok);
                    valid = jwttoken.ValidTo <= DateTime.Now;
                }
                return valid;
            }
            catch (Exception ex)
            {
                return valid;
            }

        }
        private async Task ReturnErrorResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.StartAsync();
        }
        #endregion

    }
}
