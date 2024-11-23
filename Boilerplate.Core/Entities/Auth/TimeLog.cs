

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Core.Entities.Auth
{
    [Table("time_logs")]
    public class TimeLog
    {
        [Key]
        [Column("id")]
        public long Id { get; set; } = 0;
        [Column("check_time")]
        public DateTime CheckTime { get; set; }
        [Column("created_by")]
        public string? CreatedBy { get; set; }

    }
}
