using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Contracts.DTOs.Getter.Visits
{
    public record VisitsGetterDto
    {
        public long Id { get; init; }
        public long PatientId { get; init; }
        public string PatientName { get; init; }
        public string Email { get; init; }
        public string Gender { get; init; }
        public string ExamType { get; init; }
        public string ExamStatus { get; init; }
        public string Comment { get; init; }
        public DateTime Birthdate { get; init; }
        public DateTime? CreatedAt { get; init; }


    }
}
