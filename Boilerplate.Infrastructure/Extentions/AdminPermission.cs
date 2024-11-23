using Boilerplate.Shared.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace Boilerplate.Infrastructure.Extentions
{
    public class AdminPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string AccessToken = filterContext.HttpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    var tok = AccessToken.Replace("Bearer ", "");
                    var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
                    var Roles = jwttoken.Claims.Where(claim => claim.Type == "roles").Select(q => q.Value).ToList();
                    if (Roles != null && Roles.Count() > 0 && Roles.Contains(Shared.Consts.Roles.Admin))
                    {
                        base.OnActionExecuting(filterContext);
                        return;

                    }
                }
                filterContext.Result = new UnauthorizedResult();

            }
            catch (Exception ex)
            {
                filterContext.Result = new UnauthorizedResult();
            }

        }
    }
}

