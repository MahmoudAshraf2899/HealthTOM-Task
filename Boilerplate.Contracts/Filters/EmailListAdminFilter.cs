using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class EmailListAdminFilter : Pager
    {
        public string Email { get; set; }
        public long? EmailListCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPublished { get; set; }
        public int? Status { get; set; }

    }
}
