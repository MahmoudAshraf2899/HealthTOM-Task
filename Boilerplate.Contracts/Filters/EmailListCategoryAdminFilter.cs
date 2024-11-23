using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class EmailListCategoryAdminFilter : Pager
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPublished { get; set; }
        public int? Status { get; set; }

    }
}
