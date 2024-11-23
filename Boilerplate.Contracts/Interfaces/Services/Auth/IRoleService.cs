using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.Role;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleUser;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.Interfaces.Custom;

namespace Boilerplate.Contracts.IServices.Services.Auth
{
    public interface IRoleService
    {
        Task<IHolderOfDTO> GetAllAsync();
        Task<IHolderOfDTO> SearchAsync(RoleFilter filter);
        Task<IHolderOfDTO> RoleWithPermissions();
        Task<IHolderOfDTO> UpdateRoleWithPermissionAsync(RoleWithPermissionSetterDTO RoleSetterDTO);
        Task<IHolderOfDTO> AddUserToRoleAsync(RoleUsersRequest UserRolesRequest);
        Task<IHolderOfDTO> GetByIdAsync(string id);
        Task<IHolderOfDTO> UpdateAsync(RoleUpdateSetterDTO RoleSetterDTO);
        Task<IHolderOfDTO> SaveAsync(RoleSetterDTO RoleSetter);
        Task<IHolderOfDTO> Delete(string id);
    }
}
