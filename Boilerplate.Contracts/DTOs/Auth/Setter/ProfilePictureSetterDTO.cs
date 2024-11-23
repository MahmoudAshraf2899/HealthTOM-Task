using Boilerplate.Contracts.Bases;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class ProfilePictureSetterDTO : BaseUpdateSetterDTO
    {
        [Display(Name = "user_id")]
        public string UserId { get; set; }
        [Display(Name = "title")]
        public string Title { get; set; }
        [Display(Name = "mime")]
        public string Mime { get; set; }
        [Display(Name = "url")]
        public string Url { get; set; }
        [Display(Name = "file_key")]
        public string FileKey { get; set; }

    }
}