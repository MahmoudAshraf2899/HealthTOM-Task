using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities.Migrations
{
    [Table("migrations")]
    public class Migration : BaseEntityWithUpdate
    {
        [Column("file_name")]
        [Required(ErrorMessage = "Name is required")]
        public string FileName { get; set; }
        [Column("no_sheets")]
        public int NoSheets { get; set; }
        [Column("file_url")]
        public string FileUrl { get; set; }
        [Column("migration_status")]
        public string MigrationStatus { get; set; }
        [Column("error_sheet")]
        public string ErrorSheet { get; set; }
        [Column("error_row")]
        public int ErrorRow { get; set; }
        [Column("error_column")]
        public int ErrorColumn { get; set; }
        [Column("error")]
        public string Error { get; set; }
    }
}
