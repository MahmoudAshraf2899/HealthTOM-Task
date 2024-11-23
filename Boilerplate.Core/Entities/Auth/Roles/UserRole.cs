using Boilerplate.Contracts.Interfaces.Custom;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities.Auth.Roles
{
    //[Table("role_users")]
    public class UserRole : IdentityUserRole<string>, IUserInsertDTO, IUserUpdateDTO
    {
        //  public override string UserId { get; set; }
        // public override string RoleId { get; set; }
        [Column("id")]
        public long Id { get; set; }
        [Column("created_by")]
        public string CreatedBy { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_by")]
        public string UpdatedBy { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

    }
}

