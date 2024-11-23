using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.IServices.Repositories.Auth;
using Boilerplate.Core.Entities.Auth;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;
using Boilerplate.Shared.Consts;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Services.Repositories.Auth
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BoilerplateDBContext _db;

        public UserRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }
        public async Task<User> GetUserByIdAdminQuery(string id)
        {
            var query = _db.Users
                .AsNoTracking()
                .Where(q => q.Id == id)
                .Include(q => q.UserRoles).ThenInclude(q => q.Role).ThenInclude(q => q.RoleTranslations)                  
                .AsQueryable();

            return query.FirstOrDefault();
        }
        public async Task<IQueryable<User>> buildUserQueryAsync(UserFilter userFilter)
        {
            var query = _db.Users
                 .AsNoTracking()
                 .Include(q => q.UserRoles).ThenInclude(q => q.Role).ThenInclude(q => q.RoleTranslations)                  
                 .AsQueryable();

            if (userFilter is not null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(userFilter.Id))
                        query = query.Where(x => x.Id == userFilter.Id);

                    if (!string.IsNullOrEmpty(userFilter.FullName))
                        query = query.Where(x => (x.FirstName + " " + x.LastName).Contains(userFilter.FullName));

                    if (!string.IsNullOrEmpty(userFilter.Username))
                        query = query.Where(x => x.UserName.Contains(userFilter.Username));

                    if (!string.IsNullOrEmpty(userFilter.Email))
                        query = query.Where(x => x.Email.Contains(userFilter.Email));

                    if (!string.IsNullOrEmpty(userFilter.PhoneNumber))
                        query = query.Where(x => x.PhoneNumber.Contains(userFilter.PhoneNumber));

                    if (userFilter.Gender != null)
                        query = query.Where(x => x.Gender == (Gender)userFilter.Gender);

                    if (userFilter.IsBanned != null)
                        query = query.Where(x => x.IsBanned == userFilter.IsBanned);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            return query;
        }
        
        public async Task<List<User>> GetAllUsers()
        {
            var lUsers = await _db.Users
                .AsNoTracking()                 
                .Include(q => q.UserRoles).ThenInclude(q => q.Role).ThenInclude(q => q.RoleTranslations)
                .ToListAsync();
            return lUsers;
        }
        


    }
}
