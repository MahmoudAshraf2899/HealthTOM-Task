using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.IServices.Repositories.Auth.Roles;
using Boilerplate.Core.Entities.Auth.Roles;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Services.Repositories.Auth.Roles
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly BoilerplateDBContext _db;
        public RoleRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }

        public IQueryable<object> buildRoleQuery(RoleFilter roleFilter)
        {
            var query = Query();
            if (roleFilter is not null)
            {
                try
                {
                    // Where
                    if (!string.IsNullOrEmpty(roleFilter.Id))
                        query = query.Where(x => x.Id == roleFilter.Id);

                    if (!string.IsNullOrEmpty(roleFilter.Name))
                        query = query.Where(x => x.Name.Contains(roleFilter.Name));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return query;
        }
    }
}
