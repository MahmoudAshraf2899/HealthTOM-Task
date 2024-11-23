using Boilerplate.Core.Entities.Auth.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities
{
    [Table("role_permission")]
    public partial class RolePermission : BaseEntityUpdate
    {
        [Required(ErrorMessage = "Role Id is required")]
        [Column("role_id")]
        public string RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }
        [Column("module_id")]
        [Required(ErrorMessage = "Module Id is required")]
        public long ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        public virtual PermissionModule Module { get; set; }
        [Column("operation_id")]
        public long OperationId { get; set; }
    }
}
