using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        public long Id { get; set; } = 0;
    }

    public abstract class BaseEntityWithUpdate : BaseEntity
    {
        [Column("created_by")]
        public string CreatedBy { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_by")]
        public string UpdatedBy { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
    public abstract class BaseEntityUpdate : BaseEntityWithUpdate
    {
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseEntityTranslation : BaseEntityWithUpdate
    {
        [Column("locale")]
        [MaxLength(6)]
        [Required(ErrorMessage = "Locale is required")]
        public string Locale { get; set; }
    }

}