using Boilerplate.Contracts.Bases;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Roles.RoleTranslation
{
    public class RoleTranslationGetterDTO : BaseGetterDTO
    {
        public string Name { get; set; }
        public string Locale { get; set; }

    }
}
