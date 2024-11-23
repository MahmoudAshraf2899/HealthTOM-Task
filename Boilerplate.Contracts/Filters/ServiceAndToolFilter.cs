#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class ServiceAndToolFilter :Pager
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Link { get; set; }
    }
    public class ServiceAndToolAdminFilter : ServiceAndToolFilter
    {
        public bool? IsDeleted { get; set; }
        public bool? IsPublished { get; set; }
        public int? Status { get; set; }
    }
}
