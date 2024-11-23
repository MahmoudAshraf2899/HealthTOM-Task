#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class EventFilter : Pager
    {
        public string Title { get; set; }
        public string Speaker { get; set; }
        public long? EventTypeId { get; set; }
    }
    public class EventFilterAdmin : EventFilter
    {
        public long? Id { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
