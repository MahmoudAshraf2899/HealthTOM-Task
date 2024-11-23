using Boilerplate.Core.Entities;
using Boilerplate.Core.IServices.Custom.Repositories;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Repositories.Custom.Log_System
{
    internal class LogErrorRepository : GenericRepository<LogError>, ILogErrorRepository
    {
        private readonly BoilerplateDBContext _db;
        public LogErrorRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }


    }
}
