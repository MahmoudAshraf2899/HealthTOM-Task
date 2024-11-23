using AutoMapper;
using Boilerplate.Contracts.DTOs.Auth.Getter.Roles.RolePermission;
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
    public class PermissionService : BaseService<PermissionService>, IPermissionService
    {
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture,
            IHostEnvironment environment, IHttpContextAccessor httpContextAccessor, ILogger<PermissionService> logger)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
        }

        public async Task<IHolderOfDTO> GetAllAsync()
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var query = await _unitOfWork.RolePermission.GetAllAsync();
                _holderOfDTO.Add(Res.Response, _mapper.Map<IEnumerable<RolePermissionGetterDTO>>(query.ToList()));
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
                var query = await _unitOfWork.RolePermission.FirstOrDefaultAsync(q => q.Id == id);
                if (query != null)
                {
                    _holderOfDTO.Add(Res.Response, _mapper.Map<RolePermissionGetterDTO>(query));
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
        public async Task<IHolderOfDTO> GetPermissionByRoleId(string RoleId)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var roleData = await _unitOfWork.Roles.FirstOrDefaultAsync(q => q.Id == RoleId);

                if (roleData != null)
                {
                    var rolePermisson = _mapper.Map<IEnumerable<RolePermissionGetterDTO>>(await _unitOfWork.RolePermission.FindAll(q => q.RoleId == RoleId, new[] { "Role", "Module" }));
                    var result = new RoleWithPermissionGetterDTO()
                    {
                        RoleId = RoleId,
                        RoleName = roleData.Name,
                        RolePermission = rolePermisson.ToList(),
                    };
                    _holderOfDTO.Add(Res.Response, result);
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

        public async Task<IHolderOfDTO> SaveAsync(List<RolePermissionSetterDTO> RolePermissionSetterDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var lNewPermissions = new List<RolePermission>();
                var lUpdatePermissions = new List<RolePermission>();
                var lDeletePermissions = _unitOfWork.RolePermission.FindAll(q => q.RoleId == RolePermissionSetterDTO.First().RoleId).Result.ToList();

                foreach (var item in RolePermissionSetterDTO)
                {
                    if (item.Id == 0)
                    {
                        var RolePermission = _mapper.Map<RolePermission>(item);
                        AddCreateData(RolePermission);
                        lNewPermissions.Add(RolePermission);
                    }
                    else if (lDeletePermissions.Any(x => x.Id == item.Id))
                    {
                        var RolePermission = _mapper.Map<RolePermission>(item);
                        lDeletePermissions.RemoveAll(x => x.Id == item.Id);
                        AddUpdateData(RolePermission);
                        lUpdatePermissions.Add(_mapper.Map<RolePermission>(item));
                    }
                }
                if (lDeletePermissions.Count() > 0)
                {
                    _unitOfWork.RolePermission.DeleteRange(lDeletePermissions);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                }
                if (lUpdatePermissions.Count() > 0)
                {
                    _unitOfWork.RolePermission.UpdateRange(lUpdatePermissions);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                }

                if (lNewPermissions.Count() > 0)
                {
                    _unitOfWork.RolePermission.AddRange(lNewPermissions);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                }
                //_holderOfDTO.Add(Res.Response, _unitOfWork.g);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            if (lIndicators.All(x => x))
                await GetPermissionByRoleId(RolePermissionSetterDTO.FirstOrDefault().RoleId);
            else
                _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public IHolderOfDTO Update(RolePermissionSetterDTO RolePermissionSetterDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var RolePermission = _mapper.Map<RolePermission>(RolePermissionSetterDTO);
                AddUpdateData(RolePermission);
                var oRolePermission = _unitOfWork.RolePermission.Update(RolePermission);
                lIndicators.Add(_unitOfWork.Complete() > 0);
                _holderOfDTO.Add(Res.id, oRolePermission.Id);
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
                var query = _unitOfWork.RolePermission.Find(q => q.Id == id);
                if (query != null)
                {
                    _unitOfWork.RolePermission.Delete(id);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                    _logger.LogInformation(Res.message, Res.Deleted);
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
        public bool IsValidPermission(string RoleName, long ModuleId, long OperationId)
        {
            try
            {
                if (_unitOfWork.RolePermission.FirstOrDefaultAsync(q => q.Role.Name == RoleName && q.ModuleId == ModuleId && q.OperationId == OperationId).Result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                _logger.LogError(Res.message, ex.Message);
                return false;
            }
        }
        public async Task<Dictionary<string, List<long>>> GetPermissionByRolesName(List<string> RolesName)
        {
            Dictionary<string, List<long>> Roles = new Dictionary<string, List<long>>();
            List<bool> lIndicators = new List<bool>();
            try
            {
                var RolePermissionEntity = _unitOfWork.RolePermission.FindAll(q => RolesName.Contains(q.Role.Name), includes: new string[] { "Module" }).Result.Select(s => new
                {
                    Name = s.Module.Name,
                    Permission = s.OperationId
                });
                RolePermissionEntity = RolePermissionEntity.DistinctBy(q => new
                {
                    Name = q.Name,
                    Permission = q.Permission
                }).ToList();
                foreach (var role in RolePermissionEntity)
                {
                    if (!Roles.ContainsKey(role.Name))
                        Roles.Add(role.Name, new List<long> { role.Permission });
                    else
                        Roles[role.Name].Add(role.Permission);

                }
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            return Roles;
        }
    }
}
