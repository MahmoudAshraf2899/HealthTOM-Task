using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters.Auth
{
    public class UserRoleFilter : Pager
    {
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
    }
}
