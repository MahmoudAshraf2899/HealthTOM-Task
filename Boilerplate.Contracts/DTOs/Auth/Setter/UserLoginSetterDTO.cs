using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class UserLoginSetterDTO
    {
        [Display(Name = "PersonalKey"), Required(ErrorMessage = "Required"), MaxLength(100)]
        public string PersonalKey { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
}
