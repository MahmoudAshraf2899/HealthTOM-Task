using Boilerplate.Contracts.DTOs.Auth.Getter.Roles.RoleTranslation;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Roles.Role
{
    public class RoleGetterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? UserCount { get; set; }
        public string Code { get; set; }
        public bool IsSystem { get; set; } = false;
        public List<RoleTranslationGetterDTO> RoleTranslations { get; set; }
    }
}
