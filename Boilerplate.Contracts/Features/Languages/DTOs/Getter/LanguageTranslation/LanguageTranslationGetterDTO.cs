using Boilerplate.Contracts.Bases;
#nullable disable

namespace Boilerplate.Contracts.Features.Languages.DTOs.Getter.LanguageTranslation
{
    public class LanguageTranslationGetterDTO : BaseGetterUpdateTranslationDTO
    {
        public string Name { get; set; }
    }
}