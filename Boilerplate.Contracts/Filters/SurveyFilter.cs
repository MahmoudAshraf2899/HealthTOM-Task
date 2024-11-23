using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class SurveyFilter : Pager
    {
        public long? Id { get; set; }
        public string Title { get; set; }
    }
    public class SurveyAdminFilter : SurveyFilter
    {
        public bool? HomePage { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
