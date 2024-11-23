#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class GovernorateFilter : Pager
    {
        public string? Name { get; set; }
    }
    public class GovernorateAdminFilter : AdminFilterBase
    {
        public string? Name { get; set; }
    }
}
