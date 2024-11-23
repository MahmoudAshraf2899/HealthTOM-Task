using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class ChangePasswordSetterDTO
    {

        [DataType(DataType.Password)]
        [Display(Name = "OldPassword"), Required(ErrorMessage = "Required"), StringLength(256)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "NewPassword"), Required(ErrorMessage = "Required"), StringLength(256)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword"), Required(ErrorMessage = "Required"), StringLength(256), Compare("NewPassword", ErrorMessage = "Not Match")]
        public string ConfirmNewPassword { get; set; }
    }
}
