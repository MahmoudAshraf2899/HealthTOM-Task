using Boilerplate.Contracts.IServices.Repositories.Auth;
using Boilerplate.Core.Entities.Auth;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;

namespace Boilerplate.Infrastructure.Services.Repositories.Auth
{
    public class TimeLogRepository : GenericRepository<TimeLog>, ITimeLogRepository
    {
        private readonly BoilerplateDBContext _db;
        public TimeLogRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }
        //public async Task<TimeLog> GetLastCheckDate()
        //{
        //    //return await _db.TimeLogs.OrderByDescending(q => q.Id).FirstOrDefaultAsync();
        //}
    }
}
