using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class EmailConfirmationSetterDTO
    {
        [Display(Name = "PersonalKey"), Required(ErrorMessage = "Required")]
        public string PersonalKey { get; set; }

        [Display(Name = "TokenCode"), Required(ErrorMessage = "Required"), StringLength(6)]
        public string TokenCode { get; set; }
    }
}
