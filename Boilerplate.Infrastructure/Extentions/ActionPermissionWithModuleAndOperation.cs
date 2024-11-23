using Boilerplate.Contracts.IServices.Services.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace Boilerplate.Infrastructure.Extentions
{
    public class ActionPermissionWithModuleAndOperation : ActionFilterAttribute
    {
        private readonly long ModuleId;
        private readonly long OperationId;

        public ActionPermissionWithModuleAndOperation(long ModuleId, long OperationId)
        {
            this.ModuleId = ModuleId;
            this.OperationId = OperationId;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                IPermissionService _PermissionService = (IPermissionService)filterContext.HttpContext.RequestServices.GetService(typeof(IPermissionService));
                var Roles = GetUserRoles(filterContext);
                Roles.RemoveAll(q => q == "Anonamouse");
                foreach (var item in Roles)
                {
                    if (_PermissionService.IsValidPermission(item, ModuleId, OperationId))
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

        private List<string> GetUserRoles(ActionExecutingContext filterContext)
        {
            try
            {
                string AccessToken = filterContext.HttpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    var tok = AccessToken.Replace("Bearer ", "").Replace("bearer ", "");
                    var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
                    var Roles = jwttoken.Claims.Where(claim => claim.Type == "roles").Select(q => q.Value).ToList();
                    return Roles;
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }

        }
    }
}

