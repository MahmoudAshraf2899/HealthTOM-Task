using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Boilerplate.Contracts.DTOs.Auth.Setter.ForgetPassword
{
    public class ForgetPasswordSetterDTO
    {
        [Display(Name = "Email"), Required(ErrorMessage = "لأبد من إدخال هذا العنصر"), EmailAddress(ErrorMessage = "يرجى إدخال عنوان بريد إلكتروني صالح."), StringLength(128)]
        public string Email { get; set; }
    }
}
