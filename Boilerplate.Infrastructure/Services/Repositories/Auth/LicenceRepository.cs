using Boilerplate.Contracts.IServices.Repositories.Auth;
using Boilerplate.Core.Entities.Auth;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Services.Repositories.Auth
{
    public class LicenceRepository : GenericRepository<Licence>, ILicenceRepository
    {
        private readonly BoilerplateDBContext _db;
        public LicenceRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }

    }
}
