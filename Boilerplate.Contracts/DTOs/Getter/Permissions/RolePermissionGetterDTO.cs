using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Boilerplate.Contracts.DTOs.Getter
{
    public class RolePermissionGetterDTO 
    {
        public long Id { get; set; }
        [Display(Name = "role_id")]
        public string RoleId { get; set; }
        [Display(Name = "module_id")]
        public long ModuleId { get; set; }
        [Display(Name = "operation_id")]
        public long OperationId { get; set; }

        [Display(Name = "role_name")]
        public string RoleName { get; set; }
        [Display(Name = "module_name")]
        public string ModuleName { get; set; }
        [Display(Name = "operation_name")]
        public string OperationName { get; set; }
    }
}