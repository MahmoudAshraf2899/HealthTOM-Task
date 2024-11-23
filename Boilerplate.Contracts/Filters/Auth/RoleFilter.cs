using Boilerplate.Contracts.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Boilerplate.Contracts.Filters.Auth
{
    public class RoleFilter : Pager
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
