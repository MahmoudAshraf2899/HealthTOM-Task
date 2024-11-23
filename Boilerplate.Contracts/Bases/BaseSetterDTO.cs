using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Boilerplate.Contracts.Bases
{
    public abstract class BaseSetterDTO
    {
        [Required]
        public long Id { get; set; } = 0;
    }
    public abstract class BaseDeleteSetterDTO : BaseSetterDTO
    {
        public bool IsDeleted { get; set; } = false;
    }
    public abstract class BaseSetterWithUpdateDTO : BaseSetterDTO
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public abstract class BaseUpdateSetterDTO : BaseSetterWithUpdateDTO
    {
        public bool IsDeleted { get; set; }
    }
    public abstract class BaseSetterTranslationDTO
    {
        [RegularExpression("en|ar", ErrorMessage = "The locale must match to locale of system 'en' or 'ar'")]
        [Required(ErrorMessage = "Locale is required")]
        public string Locale { get; set; }
    }
    public abstract class BaseUpdateTranslationDTO : BaseSetterTranslationDTO
    {
        [Required]
        public long Id { get; set; } = 0;
    }
}