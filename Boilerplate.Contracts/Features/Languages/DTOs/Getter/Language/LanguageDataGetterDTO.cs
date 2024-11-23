using Boilerplate.Contracts.Bases;
using Boilerplate.Contracts.Features.Languages.DTOs.Getter.LanguageTranslation;
#nullable disable

namespace Boilerplate.Contracts.Features.Languages.DTOs.Getter.Language
{
    public class LanguageDataGetterDTO : BaseGetterDTO
    {
        public string Name { get; set; }
        public string Locale { get; set; }
        public string Flag { get; set; }
        public string Direction { get; set; }
        public bool IsDefault { get; set; }
        public List<LanguageTranslationDataGetterDTO> LanguageTranslations { get; set; }

    }
}