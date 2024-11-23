using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Setter
{
    public class PermissionModuleSetterDTO
    {
        [Display(Name = "name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}