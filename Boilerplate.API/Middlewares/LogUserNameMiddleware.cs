using Serilog.Context;
using System.Security.Claims;
using System.Text;

namespace Boilerplate.API.Middlewares
{
    public class LogUserNameMiddleware
    {
        private readonly RequestDelegate next;
        public LogUserNameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string requestContent = null;
            var request = context.Request;
            if (request.Method == HttpMethods.Post && request.ContentLength > 0)
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                requestContent = Encoding.UTF8.GetString(buffer);
                request.Body.Position = 0;
            }
            LogContext.PushProperty("UserName", context?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous");
            LogContext.PushProperty("RequestBody", requestContent);
            await next(context);
        }
    }
}