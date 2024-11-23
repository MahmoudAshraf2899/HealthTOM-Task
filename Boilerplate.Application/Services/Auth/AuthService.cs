using AutoMapper;
using Boilerplate.Contracts.DTOs.Auth.Getter.ForgetPassword;
using Boilerplate.Contracts.DTOs.Auth.Getter.Users;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Auth.Setter.ForgetPassword;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleUser;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Auth;
using Boilerplate.Contracts.IServices.Services.EncryptionAndDecryption;
using Boilerplate.Contracts.IServices.Services.Permissions;
using Boilerplate.Core.Bases;
using Boilerplate.Core.Entities.Auth;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Helpers;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Boilerplate.Application.Services.Auth
{
    public class AuthService : BaseService<AuthService>, IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServer _server;
        private readonly IPermissionService _PermissionService;
        private readonly IUserService _userService;
        private readonly IEncryptionAndDecryptionService _encryptionAndDecryptionService;
        public const string numbers = "0123456789";
        const int maxFailedLogins = 5;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture, IHostEnvironment environment, RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager, IServer server, IUserService userService, IHttpContextAccessor httpContextAccessor,
            IPermissionService PermissionService, IEncryptionAndDecryptionService encryptionAndDecryptionService, ILogger<AuthService> logger)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
            _server = server;
            _PermissionService = PermissionService;
            _encryptionAndDecryptionService = encryptionAndDecryptionService;
        }

        public async Task<IHolderOfDTO> RegisterUserAsync(UserRegisterSetterDTO setterDTO)
        {
            if (string.IsNullOrEmpty(setterDTO.Email))
                return ErrorMessage(_culture.SharedLocalizer["Email is required"].Value);
            if (!ObjectUtils.IsValidEmail(setterDTO.Email))
                return ErrorMessage(_culture.SharedLocalizer["Email is not correct"].Value);
            if (await _userManager.FindByEmailAsync(setterDTO.Email) is not null)
                return ErrorMessage(_culture.SharedLocalizer["Email is already registered"].Value);
            if (!string.IsNullOrEmpty(setterDTO.Username) && await _userManager.FindByNameAsync(setterDTO.Username) is not null)
                return ErrorMessage(_culture.SharedLocalizer["Username is already registered"].Value);

            var user = _mapper.Map<User>(setterDTO);
            var resultUser = await _userManager.CreateAsync(user, setterDTO.Password);
             
            _logger.LogInformation(Res.message, Res.Added);
            _holderOfDTO.Add(Res.state, true);
            return _holderOfDTO;
        }
        public async Task<IHolderOfDTO> LoginAsync(UserLoginSetterDTO setterDTO)
        {
            _holderOfDTO.Clear();
            var user = await getUserAsync(setterDTO.PersonalKey);
            if (user.AccessFailedCount >= maxFailedLogins)
                return ErrorMessage("Account locked due to too many failed login attempts, contact support."); ;
            if (user is null || !await _userManager.CheckPasswordAsync(user, setterDTO.Password))
            {
                UpdateFailedLoginCount(user);
                return ErrorMessage(_culture.SharedLocalizer["Personal Key or Password is incorrect"].Value);
            }
            if (user.IsBanned)
                return ErrorMessage(_culture.SharedLocalizer["User is Inactive"].Value);

            _logger.LogInformation(Res.message, LogMessages.Login);
            return await BuildUserAuthAsync(user);
        }
        public async Task<IHolderOfDTO> LoginAdminAsync(UserLoginSetterDTO setterDTO)
        {
            var user = await getUserAsync(setterDTO.PersonalKey);
            _holderOfDTO.Clear();
            if (user.AccessFailedCount >= maxFailedLogins)
                return ErrorMessage("Account locked due to too many failed login attempts, contact support."); ;
            if (user is null || !await _userManager.CheckPasswordAsync(user, setterDTO.Password))
            {
                UpdateFailedLoginCount(user);
                return ErrorMessage(_culture.SharedLocalizer["Personal Key or Password is incorrect"].Value);
            }
            if (user.IsBanned)
                return ErrorMessage(_culture.SharedLocalizer["User is Inactive"].Value);

            _logger.LogInformation(Res.message, LogMessages.Login);
            return await BuildAdminAuthAsync(user);
        }         
        public async Task<IHolderOfDTO> AddUserToRoleAsync(RoleUserSetterDTO userRoleSetterDTO)
        {
            var user = await _userManager.FindByIdAsync(userRoleSetterDTO.UserId);
            var roleName = await getRoleNameAsync(userRoleSetterDTO.RoleId);
            if (user is null || roleName is null)
                return ErrorMessage(_culture.SharedLocalizer["Invalid user ID or Role"].Value);
            if (await _userManager.IsInRoleAsync(user, roleName))
                return ErrorMessage(_culture.SharedLocalizer["User already assigned to this role"].Value);
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
                return ErrorMessage(_culture.SharedLocalizer["Something went wrong when tring to add role to user"].Value);
            _holderOfDTO.Add(Res.state, true);
            return _holderOfDTO;
        }
        public async Task<IHolderOfDTO> GetRoleUsersAsync(string roleName)
        {
            var lUsers = await _userManager.GetUsersInRoleAsync(roleName);
            if (lUsers is null || lUsers.Count() == 0)
                return ErrorMessage(_culture.SharedLocalizer["This role does not have users"].Value);
            _holderOfDTO.Add(Res.Response, _mapper.Map<IEnumerable<UserSetterDTO>>(lUsers));
            _holderOfDTO.Add(Res.state, true);
            return _holderOfDTO;
        }        

        #region Helper Methods
        private void UpdateFailedLoginCount(User? user)
        {
            user.AccessFailedCount++;
            var oData = _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
        }
         
        private async Task<User> getUserAsync(string personalKey)
        {
            User user = null;
            if (ObjectUtils.IsPhoneNumber(personalKey))
            {
                user = await _userManager.Users.Where(x => x.PhoneNumber == personalKey).SingleOrDefaultAsync();
            }
            else if (ObjectUtils.IsValidEmail(personalKey))
            {
                user = await _userManager.FindByEmailAsync(personalKey);
            }
            else
            {
                user = await _userManager.FindByNameAsync(personalKey);
            }
            return user;
        }

        private async Task<string> getRoleNameAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is not null)
                return role.Name;
            return null;
        }
        private async Task<JwtSecurityToken> CreateAccessTokenAsync(User user)
        {
            // Get User Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            // Get User Roles and add its to User Claims
            var userRoles = (List<string>)await _userManager.GetRolesAsync(user);
            userClaims.Add(new Claim("roles", "Anonamouse"));
            userClaims.Add(new Claim("roles", "Anonamouse"));
            if (userRoles is not null && userRoles.Count() > 0)
            {
                foreach (var userRole in userRoles)
                    userClaims.Add(new Claim("roles", userRole));
            }

            // Add Some User Claims
            var claims = new[]
            {
                new Claim("uid", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKeys.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JWTKeys.Issuer,
                audience: JWTKeys.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(JWTKeys.DurationInHours),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private async Task<IHolderOfDTO> BuildUserAuthAsync(User user)
        {
            var jwtSecurityToken = await CreateAccessTokenAsync(user);            
            var existUser = _mapper.Map<UserAuthGetterDTO>(user);
            existUser.AccessToken = _encryptionAndDecryptionService.EncryptData(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            existUser.AccessTokenExpiration = jwtSecurityToken.ValidTo;
            _holderOfDTO.Add(Res.state, true);
            _holderOfDTO.Add(Res.isConfirmed, true);
            _holderOfDTO.Add(Res.Response, existUser);
            return _holderOfDTO;
        }
        private async Task<IHolderOfDTO> BuildAdminAuthAsync(User user)
        {
            var rolesList = (List<string>)await _userManager.GetRolesAsync(user);
            var permissionList = await _PermissionService.GetPermissionByRolesName(rolesList);
            if (permissionList.Count == 0)
            {
                _holderOfDTO.Clear();
                return ErrorMessage(_culture.SharedLocalizer["Only admin account can login"].Value);
            }
            var jwtSecurityToken = await CreateAccessTokenAsync(user);
            var existUser = _mapper.Map<AdminAuthGetterDTO>(user);
            existUser.AccessToken = _encryptionAndDecryptionService.EncryptData(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            existUser.AccessTokenExpiration = jwtSecurityToken.ValidTo;
            existUser.Roles = rolesList;
            existUser.Permissions = permissionList;

            _holderOfDTO.Add(Res.state, true);
            _holderOfDTO.Add(Res.isConfirmed, true);
            _holderOfDTO.Add(Res.Response, existUser);
            return _holderOfDTO;

        }          

        #endregion
    }
}
