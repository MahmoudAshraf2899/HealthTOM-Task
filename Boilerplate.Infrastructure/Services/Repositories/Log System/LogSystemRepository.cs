using Boilerplate.Core.Entities;
using Boilerplate.Core.IServices.Custom.Repositories;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Repositories.Custom.Log_System
{
    internal class LogSystemRepository : GenericRepository<LogSystem>, ILogSystemRepository
    {
        private readonly BoilerplateDBContext _db;
        public LogSystemRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }


    }
}
