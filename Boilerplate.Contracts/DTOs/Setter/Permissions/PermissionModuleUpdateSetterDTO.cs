using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Setter
{
    public class PermissionModuleUpdateSetterDTO
    {
        public long Id { get; set; }
        [Display(Name = "name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}