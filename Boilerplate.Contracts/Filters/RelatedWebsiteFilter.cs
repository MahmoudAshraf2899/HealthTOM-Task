#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class RelatedWebsiteFilter : Pager
    {
        public long? Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public long? WebsiteTypeId { get; set; }


    }
    public class RelatedWebsiteAdminFilter : RelatedWebsiteFilter
    {
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
