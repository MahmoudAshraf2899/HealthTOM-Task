using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Setter
{
    public class RolePermissionUpdateDTO
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "module_id")]
        public long ModuleId { get; set; }

        [Required]
        [Display(Name = "operation_id")]
        public long OperationId { get; set; }
    }
}