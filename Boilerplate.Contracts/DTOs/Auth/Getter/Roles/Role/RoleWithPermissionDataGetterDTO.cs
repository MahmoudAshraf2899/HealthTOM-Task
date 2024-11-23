using Boilerplate.Contracts.DTOs.Auth.Getter.Roles.RoleTranslation;
using Boilerplate.Contracts.DTOs.Getter;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Roles.Role
{
    public class RoleWithPermissionDataGetterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? UserCount { get; set; }
        public string Code { get; set; }
        public bool IsSystem { get; set; } = false;
        public List<RolePermissionGetterDTO> RolePermissions { get; set; }
        public List<RoleTranslationGetterDTO> RoleTranslations { get; set; }
    }
}
