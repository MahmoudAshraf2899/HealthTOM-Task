using Boilerplate.Contracts.IServices.Services.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Boilerplate.Infrastructure.Extentions
{
    public class UpdateActionPermissionWithModuleAndOperation : ActionFilterAttribute
    {
        private readonly long ModuleId;
        private readonly long OperationId;

        public UpdateActionPermissionWithModuleAndOperation(long ModuleId, long OperationId)
        {
            this.ModuleId = ModuleId;
            this.OperationId = OperationId;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var obj = filterContext.ActionArguments.Values;
            try
            {
                CheckStatus(obj, filterContext);
            }
            catch (Exception ex)
            {
                try
                {
                    foreach (var objItem in obj)
                        CheckStatus(objItem, filterContext);
                }
                catch (Exception ex2)
                {
                    filterContext.Result = new UnauthorizedResult();
                }
            }
        }

        private void CheckStatus(Object obj, ActionExecutingContext filterContext)
        {
            long status = 0;
            var json = JsonConvert.SerializeObject(obj);

            var dictionary = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

            foreach (var key in dictionary)
            {
                status = (long)key["Status"];
            }

            IPermissionService _PermissionService = (IPermissionService)filterContext.HttpContext.RequestServices.GetService(typeof(IPermissionService));

            var Roles = GetUserRoles(filterContext);
            Roles.RemoveAll(q => q == "Anonamouse");
            foreach (var item in Roles)
            {
                if (status == 2)
                {
                    if (_PermissionService.IsValidPermission(item, ModuleId, 6))
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }
                }
                else if ((status == 3))
                {
                    if (_PermissionService.IsValidPermission(item, ModuleId, 7))
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }
                }
                else if ((status != 2 && status != 3))
                {
                    if (OperationId == 2)
                    {
                        if (_PermissionService.IsValidPermission(item, ModuleId, 2))
                        {
                            base.OnActionExecuting(filterContext);
                            return;
                        }
                    }
                    if (OperationId == 3)
                    {
                        if (_PermissionService.IsValidPermission(item, ModuleId, 3))
                        {
                            base.OnActionExecuting(filterContext);
                            return;
                        }
                    }
                }
            }
            filterContext.Result = new UnauthorizedResult();
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