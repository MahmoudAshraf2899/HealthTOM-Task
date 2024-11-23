using AutoMapper;
using Boilerplate.Application.Extentions;
using Boilerplate.Contracts.DTOs.Auth.Getter;
using Boilerplate.Contracts.DTOs.Auth.Getter.Users;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Getter.Lookups;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.Helpers;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Auth;
using Boilerplate.Contracts.IServices.Services.PasswordGeneration;
using Boilerplate.Core.Bases;
using Boilerplate.Core.Entities.Auth;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Helpers;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Boilerplate.Application.Services.Auth
{
    public class UserService : BaseService<UserService>, IUserService
    {
        private UserManager<User> _userManager;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();
        private readonly IHostingEnvironment _HostingEnvironment;
        private readonly IPasswordGenerationService _passwordGeneration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture, IHostEnvironment environment, IPasswordGenerationService passwordGeneration,
              UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, ILogger<UserService> logger)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
            _userManager = userManager;
            _HostingEnvironment = hostingEnvironment;
            _passwordGeneration = passwordGeneration;

        }

        public async Task<IHolderOfDTO> SearchAsync(UserFilter userFilter)
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var query = await _unitOfWork.Users.buildUserQueryAsync(userFilter);
                var users = new List<UserAdminDataGetterDTO>();
                foreach (var item in query)
                    users.Add(await SetUserRoles(item));
                int totalCount = await query.CountAsync();
                var page = new Pager();
                page.Set(userFilter.PageSize, userFilter.CurrentPage, totalCount);
                _holderOfDTO.Add(Res.page, page);
                query = query.AddPage(page.Skip, page.PageSize);
                _holderOfDTO.Add(Res.Response, users);
                _logger.LogInformation(Res.message, Res.DataFetch);
                lIndicator.Add(true);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicator, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));
            return _holderOfDTO;
        }
        public async Task<IHolderOfDTO> GetByIdAsync(string userId)
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var query = await _unitOfWork.Users.GetUserByIdAdminQuery(userId);
                if (query != null)
                {
                    UserAdminDataGetterDTO user = await SetUserRoles(query);
                    _logger.LogInformation(Res.message, Res.DataFetch);
                    _holderOfDTO.Add(Res.Response, user);
                    lIndicator.Add(true);
                }
                else
                    NotFoundError(lIndicator);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicator, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));
            return _holderOfDTO;
        }


        #region Helper Methods
        private async Task<UserAdminDataGetterDTO> SetUserRoles(User query)
        {
            var user = _mapper.Map<UserAdminDataGetterDTO>(query);
            var UserRoles = _unitOfWork.UserRoles.FindAll(q => q.UserId == user.Id).Result.Select(q => q.RoleId);
            var roles = await _unitOfWork.Roles.FindAll(x => UserRoles.Contains(x.Id), new[] { "RoleTranslations" });
            user.Roles = _mapper.Map<List<LookupStringGetterDTO>>(roles);
            return user;
        }
        #endregion
    }
}
