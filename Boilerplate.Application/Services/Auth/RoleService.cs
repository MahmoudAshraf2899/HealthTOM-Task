using AutoMapper;
using Boilerplate.Application.Extentions;
using Boilerplate.Contracts.DTOs.Auth.Getter.Roles.Role;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.Role;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleUser;
using Boilerplate.Contracts.DTOs.Getter;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.Helpers;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Auth;
using Boilerplate.Contracts.IServices.Services.Permissions;
using Boilerplate.Core.Bases;
using Boilerplate.Core.Entities;
using Boilerplate.Core.Entities.Auth.Roles;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Application.Services.Auth
{
    public class RoleService : BaseService<RoleService>, IRoleService
    {
        private readonly IPermissionService _PermissionService;

        public RoleService(IPermissionService PermissionService, IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture, IHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor, ILogger<RoleService> logger)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
            _PermissionService = PermissionService;
        }

        public async Task<IHolderOfDTO> GetAllAsync()
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var query = await _unitOfWork.Roles.FindAllAsync(new[] { "RoleTranslations" });
                _holderOfDTO.Add(Res.Response, _mapper.Map<IEnumerable<RoleGetterDTO>>(query));
                lIndicator.Add(true);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicator, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));
            return _holderOfDTO;
        }
        public async Task<IHolderOfDTO> SearchAsync(RoleFilter filter)
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var query = _unitOfWork.Roles.buildRoleQuery(filter);
                int totalCount = query.Count();
                var page = new Pager();
                page.Set(filter.PageSize, filter.CurrentPage, totalCount);
                _holderOfDTO.Add(Res.page, page);
                query = query.AddPage(page.Skip, page.PageSize);
                _holderOfDTO.Add(Res.Response, _mapper.Map<IEnumerable<RoleGetterDTO>>(query));
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
        public async Task<IHolderOfDTO> RoleWithPermissions()
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var query = await _unitOfWork.Roles.GetAllAsync();
                var Permission = _mapper.Map<IEnumerable<RolePermissionGetterDTO>>(await _unitOfWork.RolePermission.FindAllAsync(new string[] { "Role", "Module" }));
                var result = _mapper.Map<IEnumerable<RoleWithPermissionDataGetterDTO>>(query);
                foreach (var item in result)
                {
                    item.RolePermissions = Permission.Where(q => item.Name == q.RoleName).ToList();
                }
                _holderOfDTO.Add(Res.Response, result);
                lIndicator.Add(true);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicator, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));
            return _holderOfDTO;
        }
        public async Task<IHolderOfDTO> GetByIdAsync(string id)
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var roleFilter = new RoleFilter() { Id = id };
                _holderOfDTO.Add(Res.Response, _mapper.Map<RoleGetterDTO>(await _unitOfWork.Roles.buildRoleQuery(roleFilter).SingleOrDefaultAsync()));
                lIndicator.Add(true);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicator, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));

            return _holderOfDTO;
        }
        public async Task<IHolderOfDTO> SaveAsync(RoleSetterDTO setterDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                if (!await _unitOfWork.Roles.CheckIfEntityExist(q => q.Name == setterDTO.Name))
                {
                    var dbSetterDTO = _mapper.Map<Role>(setterDTO);
                    AddCreatedData(dbSetterDTO);
                    if (dbSetterDTO.RoleTranslations.Count > 0)
                        foreach (var item in dbSetterDTO.RoleTranslations)
                            AddCreateData(item);
                    var oDate = await _unitOfWork.Roles.AddAsync(dbSetterDTO);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                    _logger.LogInformation(Res.message, LogMessages.Added);
                    _holderOfDTO.Add(Res.Response, oDate.Id);
                }
                else
                    ItemAlreadyFound(lIndicators);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }
        public async Task<IHolderOfDTO> UpdateRoleWithPermissionAsync(RoleWithPermissionSetterDTO setterDTO)
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                Role role = new Role();
                if (string.IsNullOrEmpty(setterDTO.Id))
                {
                    if (!await _unitOfWork.Roles.CheckIfEntityExist(q => q.Name == setterDTO.Name))
                    {
                        role = await AddRole(setterDTO);
                        lIndicator.Add(_unitOfWork.Complete() > 0);
                        _holderOfDTO.Add(Res.id, role.Id);
                        if (setterDTO.RolePermissions != null && setterDTO.RolePermissions.Count() > 0)
                            AddNewPermissionData(_mapper.Map<List<RolePermission>>(setterDTO.RolePermissions), role.Id);
                    }
                    else
                        ItemAlreadyFound(lIndicator);
                }
                else
                {
                    role = await _unitOfWork.Roles.FirstOrDefaultAsync(q => q.Id == setterDTO.Id);
                    if (!(await _unitOfWork.Roles.CheckIfEntityExist(q => q.Name == setterDTO.Name)) || (await _unitOfWork.Roles.CheckIfEntityExist(q => q.Name == setterDTO.Name)
                      && await _unitOfWork.Roles.CheckIfEntityExist(q => q.Id == setterDTO.Id && q.Name == setterDTO.Name)))
                    {
                        role = await UpdateRole(setterDTO, role);
                        lIndicator.Add(_unitOfWork.Complete() > 0);
                        _holderOfDTO.Add(Res.id, role.Id);
                        var dbSetterDTO = UpdateRelatedData(_mapper.Map<List<RolePermission>>(setterDTO.RolePermissions), role.Id);
                        if (dbSetterDTO != null && dbSetterDTO.Count() > 0)
                        {
                            var oData = _unitOfWork.RolePermission.UpdateRange(dbSetterDTO);
                            _unitOfWork.Complete();
                        }
                    }
                    else
                        ItemAlreadyFound(lIndicator);
                }
            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                _logger.LogError(Res.message, ex.Message);
                lIndicator.Add(false);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));
            return _holderOfDTO;
        }

        public async Task<IHolderOfDTO> UpdateAsync(RoleUpdateSetterDTO updateDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var oldObj = await _unitOfWork.Roles.FirstOrDefaultAsync(q => q.Id == updateDTO.Id, new[] { "RoleTranslations" });
                if (oldObj != null)
                {
                    if (!(await _unitOfWork.Roles.CheckIfEntityExist(q => q.Name == updateDTO.Name)) || (await _unitOfWork.Roles.CheckIfEntityExist(q => q.Name == updateDTO.Name)
                      && await _unitOfWork.Roles.CheckIfEntityExist(q => q.Id == updateDTO.Id && q.Name == updateDTO.Name)))
                    {
                        var dbSetterDTO = _mapper.Map<Role>(updateDTO);
                        AddUpdateDefaultData(dbSetterDTO);
                        dbSetterDTO.RoleTranslations = UpdateRelatedData(dbSetterDTO.RoleTranslations, oldObj.Id);
                        if (oldObj.RoleTranslations.Count() > 0)
                            foreach (var item in oldObj.RoleTranslations)
                                AddUpdateTranslationDefaultData(item);
                        var oData = await _unitOfWork.Roles.UpdateAsync(dbSetterDTO);
                        lIndicators.Add(_unitOfWork.Complete() > 0);
                        _holderOfDTO.Add(Res.Response, oData.Id);
                        _logger.LogInformation(Res.message, Res.Updated);
                    }
                    else
                        ItemAlreadyFound(lIndicators);
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
        public async Task<IHolderOfDTO> Delete(string id)
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var oldObj = await _unitOfWork.Roles.FirstOrDefaultAsync(q => q.Id == id);
                if (!oldObj.IsSystem)
                {
                    _unitOfWork.Roles.Delete(id);
                    lIndicator.Add(_unitOfWork.Complete() > 0);
                    _logger.LogInformation(Res.message, Res.Deleted);
                }
                else
                    ErrorMessage(lIndicator, Res.CannotDeleteSystemData);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicator, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));
            return _holderOfDTO;
        }

        public async Task<IHolderOfDTO> AddUserToRoleAsync(RoleUsersRequest UserRolesRequest)
        {
            List<bool> lIndicator = new List<bool>();
            try
            {
                var OldUserRoles = _unitOfWork.UserRoles.FindAll(q => q.UserId == UserRolesRequest.UserId).Result;
                if (OldUserRoles != null && OldUserRoles.Count() > 0)
                {
                    _unitOfWork.UserRoles.DeleteRange(OldUserRoles);
                    lIndicator.Add(_unitOfWork.Complete() > 0);
                }
                var identityRoles = new List<IdentityUserRole<string>>();
                foreach (var item in UserRolesRequest.RoleIds)
                {
                    var identityUserRole = new IdentityUserRole<string>();
                    if (!string.IsNullOrEmpty(item))
                    {
                        identityUserRole = new IdentityUserRole<string>() { RoleId = item, UserId = UserRolesRequest.UserId };
                        identityRoles.Add(identityUserRole);
                    }
                }
                if (UserRolesRequest.RoleIds.Any())
                {
                    _unitOfWork.UserRoles.AddRange(identityRoles);
                    lIndicator.Add(_unitOfWork.Complete() > 0);
                }
                _logger.LogInformation(Res.message, Res.Added);
                lIndicator.Add(true);
            }
            catch (Exception ex)
            {
                ExceptionError(lIndicator, ex.Message);
            }
            _holderOfDTO.Add(Res.state, lIndicator.All(x => x));
            return _holderOfDTO;
        }


        #region Helper Method
        private async Task<Role> AddRole(RoleWithPermissionSetterDTO RoleSetterDTO)
        {
            Role role = new Role() { Id = Guid.NewGuid().ToString(), Name = RoleSetterDTO.Name, NormalizedName = RoleSetterDTO.Name.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() };
            AddCreatedData(role);
            role = await _unitOfWork.Roles.AddAsync(role);
            return role;
        }
        private async Task<Role> UpdateRole(RoleWithPermissionSetterDTO RoleSetterDTO, Role role)
        {
            role.Name = RoleSetterDTO.Name;
            role.NormalizedName = RoleSetterDTO.Name.ToUpper();
            AddUpdatedData(role);
            role = await _unitOfWork.Roles.UpdateAsync(role);
            _logger.LogInformation(Res.message, Res.Updated);
            return role;
        }
        private ICollection<RoleTranslation> UpdateRelatedData(ICollection<RoleTranslation> RoleUpdate, string id)
        {
            var oldObj = _unitOfWork.RoleTranslations.FindAllNoTrack(q => q.RoleId == id);
            var updatedData = RoleUpdate.Where(q => q.Id != 0).ToList();
            RemoveRelatedData(oldObj, updatedData);
            AddNewRelatedData(RoleUpdate, id);
            return updatedData;
        }
        private void AddNewRelatedData(ICollection<RoleTranslation> update, string id)
        {
            var NewData = update.Where(q => q.Id == 0).ToList();
            foreach (var item in NewData)
            {
                AddCreateData(item);
                item.RoleId = id;
            }
            _unitOfWork.RoleTranslations.AddRange(NewData);
            _unitOfWork.Complete();
        }
        private void RemoveRelatedData(IEnumerable<RoleTranslation> oldObj, ICollection<RoleTranslation> updatedData)
        {
            var DeletedData = new List<RoleTranslation>();
            foreach (var item in oldObj)
            {
                var obj = updatedData.FirstOrDefault(q => q.Id == item.Id);
                if (obj == null)
                    DeletedData.Add(item);
            }
            if (DeletedData != null || DeletedData.Count() > 0)
            {
                _unitOfWork.RoleTranslations.DeleteRange(DeletedData);
                _unitOfWork.Complete();
            }
        }
        private void AddUpdateDefaultData(Role dbSetterDTO)
        {
            var oldObj = _unitOfWork.Roles.Find(q => q.Id == dbSetterDTO.Id);
            if (oldObj != null)
            {
                dbSetterDTO.CreatedBy = oldObj.CreatedBy;
                dbSetterDTO.CreatedAt = oldObj.CreatedAt;
                AddUpdatedData(dbSetterDTO);
            }
        }
        private void AddUpdateTranslationDefaultData(RoleTranslation dbSetterDTO)
        {
            var oldObj = _unitOfWork.RoleTranslations.Find(q => q.Id == dbSetterDTO.Id);
            if (oldObj != null)
            {
                dbSetterDTO.CreatedBy = oldObj.CreatedBy;
                dbSetterDTO.CreatedAt = oldObj.CreatedAt;
                AddUpdateData(dbSetterDTO);
            }
        }
        private void AddCreatedData(Role dbSetter)
        {
            dbSetter.CreatedBy = GetUserId();
            dbSetter.CreatedAt = DateTime.Now;
            AddUpdatedData(dbSetter);
        }
        private void AddUpdatedData(Role dbSetter)
        {
            dbSetter.UpdatedBy = GetUserId();
            dbSetter.UpdatedAt = DateTime.Now;
        }
        private ICollection<RolePermission> UpdateRelatedData(List<RolePermission> data, string id)
        {
            var oldObj = _unitOfWork.RolePermission.FindAllNoTrack(q => q.RoleId == id);
            var updatedData = data.Where(q => q.Id != 0).ToList();
            RemovePermissionData(oldObj.ToList(), updatedData);
            AddNewPermissionData(data, id);
            return updatedData;
        }
        private void AddNewPermissionData(List<RolePermission> update, string id)
        {
            var newData = update.Where(q => q.Id == 0).ToList();
            foreach (var item in newData)
            {
                AddCreateData(item);
                item.RoleId = id;
            }
            _unitOfWork.RolePermission.AddRange(newData);
            _unitOfWork.Complete();
        }
        private void RemovePermissionData(List<RolePermission> oldObj, List<RolePermission> updatedData)
        {
            var DeletedData = new List<RolePermission>();
            foreach (var item in oldObj)
            {
                var obj = updatedData.FirstOrDefault(q => q.Id == item.Id);
                if (obj == null)
                    DeletedData.Add(item);
            }
            if (DeletedData != null || DeletedData.Count() > 0)
            {
                _unitOfWork.RolePermission.DeleteRange(DeletedData);
                _unitOfWork.Complete();
            }
        }

        #endregion
    }
}
