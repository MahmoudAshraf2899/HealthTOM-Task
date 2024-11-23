using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleTranslation;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Boilerplate.Contracts.DTOs.Auth.Setter.Roles.Role
{
    public class RoleSetterDTO
    {

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public List<RoleTranslationSetterDTO > RoleTranslations { get; set; }

    }
}
