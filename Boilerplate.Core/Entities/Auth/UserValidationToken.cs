using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Core.Entities.Auth
{
    [Table("user_validation_tokens", Schema = "security")]
    [Owned]
    public class UserValidationToken
    {
        [Key]
        [Column("token_id")]
        public long TokenId { get; set; }

        [Required, MaxLength(6)]
        [Column("code")]
        public string Code { get; set; }

        [Required, MaxLength(400)]
        [Column("token")]
        public string Token { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }

        [MaxLength(200)]
        [Column("ip_adress")]
        public string? IpAdress { get; set; }

        [MaxLength(400)]
        [Column("agent")]
        public string? Agent { get; set; }
        [Column("expires_on")]
        public DateTime ExpiresOn { get; set; }
        [Column("is_expired")]
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        [Column("revoked_on")]
        public DateTime? RevokedOn { get; set; }
        [Column("is_revoked")]
        public bool IsRevoked => RevokedOn is not null;
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("is_active")]
        public bool IsActive => !IsExpired && !IsRevoked && !IsUsed;
    }
}
