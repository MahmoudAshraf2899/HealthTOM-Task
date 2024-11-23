using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Boilerplate.Core.Entities
{
    [Table("log_error")]
    public partial class LogError 
    {
        [Key]
        public long Id { get; set; } = 0;
        [Column("code")]
        public string Code { get; set; }
        [Column("message")]
        public string Message { get; set; }
        [Column("exception")]
        public string Exception { get; set; }
        [Column("log_system_id")]
        public long LogSystemId { get; set; }
        [ForeignKey(nameof(LogSystemId))]
        public virtual LogSystem LogSystem { get; set; }

    }
}
