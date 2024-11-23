#nullable disable
using Boilerplate.Contracts.Bases;
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class JournalCategoryFilter : Pager
    {
        public string? Name { get; set; }
    }
    public class JournalCategoryAdminFilter : AdminFilterBase
    {
        public string? Name { get; set; }
    }
}
