#nullable disable

using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class CategoryFilter :Pager
    {
        public string CategoryName { get; set; }
    }
    public class CategoryAdminFilter : CategoryFilter
    {
        public long? Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
        public int? Status { get; set; }

    }
}
