using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class PersonalKeySetterDTO
    {
        [Display(Name = "PersonalKey"), Required(ErrorMessage = "Required")]
        public string PersonalKey { get; set; }


    }
}
