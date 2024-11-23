using Boilerplate.Contracts.Bases;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.Features.Languages.DTOs.Setter.LanguageTranslation
{
    public class LanguageTranslationSetterDTO : BaseSetterTranslationDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}