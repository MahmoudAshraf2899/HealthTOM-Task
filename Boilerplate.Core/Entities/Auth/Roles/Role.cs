using Boilerplate.Contracts.Interfaces.Custom;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities.Auth.Roles
{
    //[Table("roles", Schema = "security")]
    public class Role : IdentityRole, IUserInsertDTO, IUserUpdateDTO
    {
        [Column("code")]
        public string Code { get; set; }

        [Column("created_by")]
        public string CreatedBy { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_by")]
        public string UpdatedBy { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("is_system")]
        public bool IsSystem { get; set; } = false;

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<RoleTranslation> RoleTranslations { get; set; }

    }
}
