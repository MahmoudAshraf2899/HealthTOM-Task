#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class SelectedIndicatorFilter : AdminFilterBase
    {
        public string Title { get; set; }
        public string Unit { get; set; }
        public string StudyPeriod { get; set; }
        public string Icon { get; set; }
        public decimal? Value { get; set; }
    }
}
