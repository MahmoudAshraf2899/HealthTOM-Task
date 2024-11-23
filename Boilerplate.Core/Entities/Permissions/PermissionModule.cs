using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities
{
    [Table("permission_modules")]
    public partial class PermissionModule : BaseEntityUpdate
    {
        [Required(ErrorMessage = "Name is required")]
        [Column("name")]
        public string Name { get; set; }

        [Column("is_system")]
        public bool IsSystem { get; set; } = false;
    }
}
