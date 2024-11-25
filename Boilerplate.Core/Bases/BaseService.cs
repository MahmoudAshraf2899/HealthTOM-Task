using AutoMapper;
using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Helpers;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Core.Entities;
using Boilerplate.Core.Entities.Auth;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Boilerplate.Core.Bases
{
    public class BaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IHolderOfDTO _holderOfDTO;
        protected readonly ICulture _culture;
        private readonly IHostEnvironment _environment;
        private readonly IHttpContextAccessor _HtpContextAccessor;
        protected readonly ILogger<T> _logger;
        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ILogger<T> logger = null, ICulture culture = null, IHostEnvironment environment = null
           , IHttpContextAccessor HttpContextAccessor = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _holderOfDTO = holderOfDTO;
            _culture = culture;
            _environment = environment;
            _HtpContextAccessor = HttpContextAccessor;
            UserDetails.userId = GetUserId();
            _logger = logger;
        }
        protected string GetUserId()
        {
            string UserId = "";
            string AccessToken = "";
            try
            {
                if (_HtpContextAccessor != null && _HtpContextAccessor.HttpContext != null && _HtpContextAccessor.HttpContext.Request != null && _HtpContextAccessor.HttpContext.Request.Headers != null)
                    AccessToken = _HtpContextAccessor.HttpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    var tok = AccessToken.Replace("Bearer ", "").Replace("bearer ", "");
                    var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
                    UserId = jwttoken.Claims.First(claim => claim.Type == "uid").Value;
                }
                return UserId;
            }
            catch (Exception ex)
            {
                return UserId;
            }
        }
        protected string GetUserEmail()
        {
            string UserEmail = "";
            string AccessToken = "";
            try
            {
                if (_HtpContextAccessor != null && _HtpContextAccessor.HttpContext != null && _HtpContextAccessor.HttpContext.Request != null && _HtpContextAccessor.HttpContext.Request.Headers != null)
                    AccessToken = _HtpContextAccessor.HttpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    var tok = AccessToken.Replace("Bearer ", "").Replace("bearer ", "");
                    var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
                    UserEmail = jwttoken.Claims.First(claim => claim.Type == "email").Value;
                }
                return UserEmail;
            }
            catch (Exception ex)
            {
                return UserEmail;
            }
        }
        protected void AddCreateData(BaseEntityWithUpdate Entity)
        {
            Entity.UpdatedAt = Entity.CreatedAt = DateTime.Now;
            Entity.CreatedBy = Entity.UpdatedBy = GetUserId();
        }
        protected void AddUpdateData(BaseEntityWithUpdate Entity)
        {
            Entity.UpdatedAt = DateTime.Now;
            Entity.UpdatedBy = GetUserId();
        }
        protected void AddHistoricalDataOfUser(User user)
        {
            user.CreatedBy = user.UpdatedBy = GetUserId();
            user.CreatedAt = user.UpdatedAt = DateTime.Now;
        }
        #region Messages
        protected bool CheckPublish(bool isPublish, int status, List<bool> lIndicators)
        {
            if (isPublish && status != (int)Status.Approve)
            {
                CannotPublish(lIndicators);
                return true;
            }
            return false;
        }
        protected void ErrorMessage(List<bool> lIndicators, string message)
        {
            _holderOfDTO.Add(Res.message, message);
            _logger.LogError(Res.message, message);
            lIndicators.Add(false);
        }
        protected IHolderOfDTO ErrorMessage(string message)
        {
            _holderOfDTO.Add(Res.state, false);
            _holderOfDTO.Add(Res.message, message);
            _logger.LogError(Res.message, message);
            return _holderOfDTO;
        }
        protected void ExceptionError(List<bool> lIndicators, string message)
        {
            _holderOfDTO.Add(Res.message, "Something Bad happened, Please contact Administrator!");
            _logger.LogError(Res.message, message);
            lIndicators.Add(false);
        }
        protected void NotFoundError(List<bool> lIndicators)
        {
            ErrorMessage(lIndicators, Res.RecNotFound);
        }
        protected void CannotPublish(List<bool> lIndicators)
        {
            ErrorMessage(lIndicators, Res.CannotPublish);
        }
        protected void ItemAlreadyFound(List<bool> lIndicators)
        {
            ErrorMessage(lIndicators, _culture.SharedLocalizer["This item is already found"].Value);
        }
        #endregion

        
       
    }
}
