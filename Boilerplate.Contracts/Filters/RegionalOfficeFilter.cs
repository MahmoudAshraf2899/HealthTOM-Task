#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class RegionalOfficeFilter : Pager
    {
        public string Name { get; set; }
        public long GovernorateId { get; set; }       
        public bool? isMainLocation { get; set; }

    }
    public class RegionalOfficeAdminFilter : AdminFilterBase
    {
        public string Name { get; set; }
        public long GovernorateId { get; set; }
        public bool isMainLocation { get; set; }

    }
}
