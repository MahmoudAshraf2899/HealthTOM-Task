using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class HappeningNowAdminFilter : Pager
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public bool? IsInternal { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }

    }
}
