using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class InternationalDayFilter : Pager
    {
        public string Title { get; set; }
    }
    public class InternationalDayFilterAdmin : InternationalDayFilter
    {
        public long? Id { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
