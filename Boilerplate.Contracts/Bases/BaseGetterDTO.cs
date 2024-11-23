#nullable disable
namespace Boilerplate.Contracts.Bases
{
    public abstract class BaseGetterDTO
    {
        public long Id { get; set; } = 0;
    }
    public abstract class BaseGetterWithUpdateDTO : BaseGetterDTO
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public abstract class BaseGetterUpdateDTO : BaseGetterWithUpdateDTO
    {
        public bool IsDeleted { get; set; }
    }
    public abstract class BaseGetterTranslationDTO : BaseGetterDTO
    {
        public string Locale { get; set; }
    }
    public abstract class BaseGetterUpdateTranslationDTO : BaseGetterWithUpdateDTO
    {
        public string Locale { get; set; }

    }
}
