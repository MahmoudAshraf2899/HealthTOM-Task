#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class NewsBarFilter : Pager
    {
        public long? Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
    }
    public class NewsBarAdminFilter : NewsBarFilter
    {
        public bool? IsInternal { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
