#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class NewsFilter : Pager
    {
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? CategoryId { get; set; }
        public long? SubSubjectId { get; set; }
    }
    public class NewsAdminFilter : NewsFilter
    {
        public long? Id { get; set; }
        public bool? HomePage { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsInternal { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
