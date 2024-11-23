#nullable disable
using Boilerplate.Contracts.Bases;
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class JournalismFilter : Pager
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? JournalName { get; set; }
        public string? Title { get; set; }
    }
    public class JournalismAdminFilter : AdminFilterBase
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? JournalName { get; set; }
        public string? Title { get; set; }
    }
}
