using Boilerplate.Contracts.Bases;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleTranslation
{
    public class RoleTranslationUpdateSetterDTO : BaseUpdateTranslationDTO
    {

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

    }
}
