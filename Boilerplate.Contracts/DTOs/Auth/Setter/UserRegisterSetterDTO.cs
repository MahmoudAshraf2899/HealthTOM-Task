using Boilerplate.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class UserRegisterSetterDTO : UserSetterDTO
    {

        [Required(ErrorMessage = "Password is Required"), StringLength(16)]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword"), Required(ErrorMessage = "ConfirmPassword is Required"), StringLength(16), Compare("Password", ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address"), StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "BirthDate")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Degree")]
        public Degree? Degree { get; set; }

        [Display(Name = "SubscriptionTypeId")]
        public long SubscriptionTypeId { get; set; }

        [Display(Name = "CountryId")]
        public long? CountryId { get; set; }
    }
}
