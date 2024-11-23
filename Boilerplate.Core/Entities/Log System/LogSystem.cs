using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

#nullable disable

namespace Boilerplate.Core.Entities
{
    [Table("log_system")]
    public partial class LogSystem 
    {
        [Key]
        public long Id { get; set; } = 0;
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("module_id")]
        public long ModuleId { get; set; }
        [Column("module_name")]
        public string ModuleName { get; set; }
        [Column("item_id")]
        public string ItemId { get; set; }
        [Column("item")]
        public string Item { get; set; }
        [Column("date_time")]
        public DateTime DateTime { get; set; } = DateTime.Now;
        [Column("action")]
        public string Action { get; set; }
        [Column("is_success")]
        public bool IsSuccess { get; set; }
        public virtual ICollection<LogError> Errors { get; set; } =new List<LogError>();

    }
}
