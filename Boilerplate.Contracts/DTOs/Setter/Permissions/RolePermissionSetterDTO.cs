using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Boilerplate.Contracts.DTOs.Setter
{
    public class RolePermissionSetterDTO
    {
        public long Id { get; set; }
        [Display(Name = "role_id")]
        public string RoleId { get; set; }
        [Display(Name = "module_id")]
        public long ModuleId { get; set; }
        [Display(Name = "operation_id")]
        public long OperationId { get; set; }
    }
}