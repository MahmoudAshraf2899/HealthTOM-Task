using Boilerplate.Contracts.IServices.Custom;
using Boilerplate.Core.Entities.Auth;

namespace Boilerplate.Contracts.IServices.Repositories.Auth
{
    public interface ITimeLogRepository : IGenericRepository<TimeLog>
    {
        // public Task<TimeLog>  GetLastCheckDate();
    }
}
