using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.IServices.Custom;
using Boilerplate.Core.Entities.Auth.Roles;

namespace Boilerplate.Contracts.IServices.Repositories.Auth.Roles
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        public IQueryable<object> buildRoleQuery(RoleFilter roleFilter);
    }
}
