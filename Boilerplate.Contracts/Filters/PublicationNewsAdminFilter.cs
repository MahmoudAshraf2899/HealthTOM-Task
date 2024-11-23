using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class PublicationNewsAdminFilter : Pager
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? HomePage { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsInternal { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
        public long? PublicationId { get; set; }
        public long? SubSubjectId { get; set; }
    }
}
