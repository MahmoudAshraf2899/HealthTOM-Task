using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Helpers;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Custom;
using Boilerplate.Core.Entities.Auth.Roles;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities.Auth
{
    [Table("users")]
    public class User : IdentityUser, IActive, IUserInsertDTO, IUserUpdateDTO
    {
        [Column("first_name")]
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Column("phone_number")]
        [Required, MaxLength(11)]
        public override string PhoneNumber { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }


        [Column("is_banned")]
        public bool IsBanned { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public List<UserValidationToken>? ValidationTokens { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
