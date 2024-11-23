#nullable disable

namespace Boilerplate.Contracts.DTOs.Getter.Lookups
{
    public class LookupGetterDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<LookupTranslationGetterDTO> Translations { get; set; }
    }
    public class LookupTranslationGetterDTO
    {
        public string Name { get; set; }
        public string Locale { get; set; }
    }
    public class LookupStringGetterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<LookupTranslationGetterDTO> Translations { get; set; }
    }
}