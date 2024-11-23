using Boilerplate.Contracts.DTOs.Setter;
using Boilerplate.Contracts.Interfaces.Custom;

namespace Boilerplate.Contracts.IServices.Services.Permissions
{
    public interface IPermissionService
    {
        public Task<IHolderOfDTO> GetAllAsync();
        public Task<IHolderOfDTO> GetByIdAsync(long id);
        public Task<IHolderOfDTO> GetPermissionByRoleId(string RoleId);
        public Task<Dictionary<string, List<long>>> GetPermissionByRolesName(List<string> RolesName);
        public Task<IHolderOfDTO> SaveAsync(List<RolePermissionSetterDTO> RolePermissionSetterDTO);
        public IHolderOfDTO Update(RolePermissionSetterDTO RolePermissionSetterDTO);
        public IHolderOfDTO Delete(long id);
        public bool IsValidPermission(string RoleName, long ModuleId, long OperationId);
    }
}
