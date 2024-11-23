using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace Boilerplate.API
{
    public class DataHub : Hub

    {
        private readonly static Dictionary<string, List<string>> Connections = new Dictionary<string, List<string>>();
        private readonly IHttpContextAccessor _HttpContextAccessor = new HttpContextAccessor();

        public override Task OnConnectedAsync()
        {
            //var userId = GetLoggedUserId();
            var userId = GetUserId();
            if (userId != null)
            {
                if (Connections.ContainsKey(userId))
                {
                    Connections[userId].Add(Context.ConnectionId);
                }
                else
                {
                    Connections[userId] = new List<string> { Context.ConnectionId };
                }
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //var userId = GetLoggedUserId();
            var userId = GetUserId();
            if (userId != null)
            {
                if (Connections.ContainsKey(userId))
                {
                    Connections[userId]?.Remove(Context.ConnectionId);
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        protected string GetUserId()
        {
            string UserId = "";
            try
            {
                string AccessToken = _HttpContextAccessor.HttpContext.Request.Query["access_token"];
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    //var tok = AccessToken.Replace("Bearer ", "");
                    var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(AccessToken);
                    UserId = jwttoken.Claims.First(claim => claim.Type == "uid").Value;
                }
                return UserId;
            }
            catch (Exception ex)
            {
                return UserId;
            }
        }
        public List<string> GetConnectionIdByUserId(string userId)
        {
            List<string> connentionIds;
            Connections.TryGetValue(userId, out connentionIds);
            if (connentionIds?.Count() > 0)
            {
                return connentionIds;
            }
            else
            {
                return null;
            }
        }
        public List<string> GetOnlineEmployeeIds()
        {
            return Connections.Keys.ToList();
        }
    }
}
