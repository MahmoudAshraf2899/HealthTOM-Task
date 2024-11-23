using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.IServices.Repositories.Auth;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;
using Microsoft.AspNetCore.Identity;

namespace Boilerplate.Infrastructure.Services.Repositories.Auth
{
    public class UserRoleRepository : GenericRepository<IdentityUserRole<string>>, IUserRoleRepository
    {
        private readonly BoilerplateDBContext _db;
        public UserRoleRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }

        public IQueryable<object> buildUserRoleQuery(UserRoleFilter userRoleFilter)
        {
            var query = Query();
            if (userRoleFilter is not null)
            {
                try
                {
                    // Where
                    if (!string.IsNullOrEmpty(userRoleFilter.UserId))
                        query = query.Where(x => x.UserId == userRoleFilter.UserId);

                    if (!string.IsNullOrEmpty(userRoleFilter.RoleId))
                        query = query.Where(x => x.RoleId == userRoleFilter.RoleId);
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
