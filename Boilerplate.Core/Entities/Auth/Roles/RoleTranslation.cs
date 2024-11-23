using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities.Auth.Roles
{
    [Table("role_translations")]
    public class RoleTranslation : BaseEntityTranslation
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("role_id")]
        public string RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }
    }
}
