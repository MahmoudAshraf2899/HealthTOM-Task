using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class ResetPasswordSetterDTO
    {
        [Required]
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password"), Required(ErrorMessage = "Required"), StringLength(256)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword"), Required(ErrorMessage = "Required"), StringLength(256), Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
