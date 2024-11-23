#nullable disable
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class TrainingProgramFilter : Pager
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Content { get; set; }
        public int? NOHours { get; set; }
        public int? NORepeat { get; set; }
        public int? NOSeats { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class TrainingProgramAdminFilter : TrainingProgramFilter
    {
        public long? Id { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }

    }
}
