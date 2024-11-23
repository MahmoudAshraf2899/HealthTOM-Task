using Boilerplate.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class UserUpdateSetterDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "phone_number"), MinLength(11, ErrorMessage = "Invalid, Phone number must be 11 numbers."),
         MaxLength(11, ErrorMessage = "Invalid, Phone number must be 11 numbers.")]
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public List<long> Groups { get; set; }
    }
}