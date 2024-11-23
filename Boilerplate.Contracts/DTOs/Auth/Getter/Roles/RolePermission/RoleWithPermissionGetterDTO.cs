using Boilerplate.Contracts.DTOs.Getter;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Roles.RolePermission
{
    public class RoleWithPermissionGetterDTO
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolePermissionGetterDTO> RolePermission { get; set; } = new List<RolePermissionGetterDTO>();

    }
}
