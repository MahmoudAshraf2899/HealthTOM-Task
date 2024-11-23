using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleTranslation;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter.Roles.Role
{
    public class RoleUpdateSetterDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public List<RoleTranslationUpdateSetterDTO> RoleTranslations { get; set; }

    }
}
