using Boilerplate.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class UserSetterDTO
    {
        [Display(Name = "FirstName"), Required(ErrorMessage = "Required"), MaxLength(100)]
        public string FirstName { get; set; }

        [Display(Name = "LastName"), Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Display(Name = "phone_number"), MinLength(11, ErrorMessage = "Invalid, Phone number must be 11 numbers."),
         MaxLength(11, ErrorMessage = "Invalid, Phone number must be 11 numbers.")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email"), Required(ErrorMessage = "Email is Required"), EmailAddress(ErrorMessage = "Invalid"), StringLength(128)]
        public string Email { get; set; }
        [Display(Name = "UserName"), Required(ErrorMessage = "Required"), StringLength(50)]
        public string Username { get; set; }
        public Gender Gender { get; set; }
    }
}