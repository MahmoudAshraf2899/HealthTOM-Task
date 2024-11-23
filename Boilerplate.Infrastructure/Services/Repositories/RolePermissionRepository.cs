using Boilerplate.Contracts.IServices.Repositories.Auth.Roles;
using Boilerplate.Core.Entities;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Services.Repositories
{
    internal class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        private readonly BoilerplateDBContext _db;
        public RolePermissionRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }


    }
}
