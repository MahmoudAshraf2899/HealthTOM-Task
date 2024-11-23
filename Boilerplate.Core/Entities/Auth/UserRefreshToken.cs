using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Core.Entities.Auth
{
    [Table("user_refresh_tokens", Schema = "security")]
    [Owned]
    public class UserRefreshToken
    {
        [Key]
        [Column("refresh_token_id")]
        public int RefreshTokenId { get; set; }

        [Required, MaxLength(400)]
        [Column("refresh_token")]
        public string RefreshToken { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }

        [Column("ip_adress")]
        [MaxLength(200)]
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
        [Column("is_active")]
        public bool IsActive => !IsExpired && !IsRevoked;
    }
}
