#nullable disable

using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class GovernoratePopulationFilter
    {
        public long GovernorateId { get; set; }
        public DateTime Date { get; set; }
    }
    public class GovernoratePopulationAdminFilter : Pager
    {
        public long GovernorateId { get; set; }
        public DateTime? Date { get; set; }
    }
}
