using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Helpers;
#nullable disable
namespace Boilerplate.Contracts.Filters
{
    public class TrainingProgramApplicationFilter :Pager
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public long? TrainingProgramId { get; set; }
        public DateTime? ApplyingDate { get; set; }
        public DateTime? BirthDate { get; set; }

    }
}
