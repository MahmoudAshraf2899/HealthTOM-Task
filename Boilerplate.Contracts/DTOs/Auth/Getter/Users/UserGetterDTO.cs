using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Interfaces.Custom;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Users
{
    public class UserGetterDTO : UserSetterDTO, IFilePathDTO
    {
        public string Id { get; set; }

        [Display(Name = "UserName"), Required(ErrorMessage = "Required")]
        public string UserName { get; set; }

        [Display(Name = "Email"), Required(ErrorMessage = "Required"), EmailAddress(ErrorMessage = "Invalid")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Path { get; set; }
        public string DisplayPath { get; set; }
        [Display(Name = "full_name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [Display(Name = "isBanned")]
        public bool IsBanned { get; set; } = false;
    }
}