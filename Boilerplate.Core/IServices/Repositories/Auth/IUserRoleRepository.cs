using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.IServices.Custom;
using Microsoft.AspNetCore.Identity;

namespace Boilerplate.Contracts.IServices.Repositories.Auth
{
    public interface IUserRoleRepository : IGenericRepository<IdentityUserRole<string>>
    {
        public IQueryable<object> buildUserRoleQuery(UserRoleFilter userRoleFilter);
    }
}
