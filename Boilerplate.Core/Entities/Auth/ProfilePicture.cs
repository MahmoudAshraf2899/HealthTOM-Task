using Boilerplate.Contracts.Helpers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Boilerplate.Core.Entities.Auth
{
    [Table("profile_pictures")]
    public partial class ProfilePicture : BaseEntityUpdate
    {
        [Column("user_id")]
        public string UserId { get; set; }
        [Required]
        [StringLength(255)]
        [Column("title")]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        [Column("mine")]
        public string Mime { get; set; }
        [NotMapped]
        private string _Url;

        [Required]
        [Column("url")]
        public string Url { get => _Url.ToHostUrl(); set => _Url = value; }

        [Required]
        [Column("file_key")]
        [StringLength(255)]
        public string FileKey { get; set; }

    }
}
