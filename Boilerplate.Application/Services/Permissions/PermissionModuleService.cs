using AutoMapper;
using Boilerplate.Contracts.DTOs.Getter;
using Boilerplate.Contracts.DTOs.Setter;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Permissions;
using Boilerplate.Core.Bases;
using Boilerplate.Core.Entities;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Application.Services
{
    public class PermissionModuleService : BaseService<PermissionModuleService>, IPermissionModuleService
    {
        public PermissionModuleService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture,
            IHostEnvironment environment, IHttpContextAccessor httpContextAccessor, ILogger<PermissionModuleService> logger)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
        }

        public async Task<IHolderOfDTO> GetAllAsync()
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var query = await _unitOfWork.PermissionModule.GetAllAsync();
                _holderOfDTO.Add(Res.Response, _mapper.Map<IEnumerable<PermissionModuleGetterDTO>>(query.ToList()));
                _logger.LogInformation(Res.message, Res.DataFetch);
                lIndicators.Add(true);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public async Task<IHolderOfDTO> GetByIdAsync(long id)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var query = await _unitOfWork.PermissionModule.FirstOrDefaultAsync(q => q.Id == id);
                if (query != null)
                {
                    _holderOfDTO.Add(Res.Response, _mapper.Map<PermissionModuleGetterDTO>(query));
                    _logger.LogInformation(Res.message, Res.DataFetch);
                    lIndicators.Add(true);
                }
                else
                    NotFoundError(lIndicators);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public async Task<IHolderOfDTO> SaveAsync(PermissionModuleSetterDTO PermissionModuleSetterDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var PermissionModule = _mapper.Map<PermissionModule>(PermissionModuleSetterDTO);
                AddCreateData(PermissionModule);
                var oPermissionModule = await _unitOfWork.PermissionModule.AddAsync(PermissionModule);
                lIndicators.Add(_unitOfWork.Complete() > 0);
                _holderOfDTO.Add(Res.id, oPermissionModule.Id);
                _logger.LogInformation(Res.message, Res.Added);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public IHolderOfDTO Update(PermissionModuleUpdateSetterDTO PermissionModuleSetterDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var PermissionModule = _mapper.Map<PermissionModule>(PermissionModuleSetterDTO);
                AddBasicDataForUpdate(PermissionModule);
                var oPermissionModule = _unitOfWork.PermissionModule.Update(PermissionModule);
                lIndicators.Add(_unitOfWork.Complete() > 0);
                _holderOfDTO.Add(Res.id, oPermissionModule.Id);
                _logger.LogInformation(Res.message, Res.Updated);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public IHolderOfDTO Delete(long id)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var oldObj = _unitOfWork.PermissionModule.Find(q => q.Id == id);
                if (oldObj != null)
                {
                    if (!oldObj.IsSystem)
                    {
                        _unitOfWork.PermissionModule.Delete(id);
                        lIndicators.Add(_unitOfWork.Complete() > 0);
                        _logger.LogInformation(Res.message, Res.Deleted);
                    }
                    else
                        ErrorMessage(lIndicators, Res.CannotDeleteSystemData);
                }
                else
                    NotFoundError(lIndicators);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }


        #region Helper Methods
        private void AddBasicDataForUpdate(PermissionModule PermissionModule)
        {
            var oldObj = _unitOfWork.PermissionModule.Find(q => q.Id == PermissionModule.Id);
            PermissionModule.CreatedBy = oldObj.CreatedBy;
            PermissionModule.CreatedAt = oldObj.CreatedAt;
            AddUpdateData(PermissionModule);
        }
        #endregion
    }
}
