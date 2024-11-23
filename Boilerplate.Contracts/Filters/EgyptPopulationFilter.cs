#nullable disable

using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class EgyptPopulationAdminFilter : Pager
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
