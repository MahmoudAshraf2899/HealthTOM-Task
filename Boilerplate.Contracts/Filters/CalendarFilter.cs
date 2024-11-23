using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class MonthCalendarFilter
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
    public class DayCalendarFilter
    {
        public DateTime Date { get; set; }
    }
    public class CalendarAdminFilter : Pager
    {
        public DateTime? Date { get; set; }
        public long? Id { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
