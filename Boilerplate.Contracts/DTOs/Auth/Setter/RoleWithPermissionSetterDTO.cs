using Boilerplate.Contracts.DTOs.Setter;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class RoleWithPermissionSetterDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        // public List<RoleTranslationSetterDTO > RoleTranslations { get; set; }
        public List<RolePermissionUpdateDTO> RolePermissions { get; set; } = new List<RolePermissionUpdateDTO>();


    }
}
