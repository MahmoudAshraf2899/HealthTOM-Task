using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.IServices.Custom;
using Boilerplate.Core.Entities.Auth;

namespace Boilerplate.Contracts.IServices.Repositories.Auth
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<IQueryable<User>> buildUserQueryAsync(UserFilter userFilter);
        Task<User> GetUserByIdAdminQuery(string id);
        
        public Task<List<User>> GetAllUsers();                    
        
    }
}
