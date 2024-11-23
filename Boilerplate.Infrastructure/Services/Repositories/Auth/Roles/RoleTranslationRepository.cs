using Boilerplate.Contracts.IServices.Repositories.Auth.Roles;
using Boilerplate.Core.Entities.Auth.Roles;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Services.Repositories.Auth.Roles
{
    public class RoleTranslationRepository : GenericRepository<RoleTranslation>, IRoleTranslationRepository
    {
        private readonly BoilerplateDBContext _db;
        public RoleTranslationRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }

    }
}
