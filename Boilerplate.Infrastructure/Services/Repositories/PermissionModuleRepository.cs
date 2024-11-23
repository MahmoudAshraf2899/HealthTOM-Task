using Boilerplate.Contracts.IServices.Repositories.Auth;
using Boilerplate.Core.Entities;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Services.Repositories
{
    internal class PermissionModuleRepository : GenericRepository<PermissionModule>, IPermissionModuleRepository
    {
        private readonly BoilerplateDBContext _db;
        public PermissionModuleRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }


    }
}
