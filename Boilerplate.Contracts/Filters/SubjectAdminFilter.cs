using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class SubjectAdminFilter : Pager
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public bool? IsInternal { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
