using Boilerplate.Contracts.DTOs.Setter.Patient;
using Boilerplate.Contracts.Interfaces.Custom;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Boilerplate.Contracts.Features.Visit.Commands
{
    public class VisitAddCommand : IRequest<IHolderOfDTO>
    {
        public string Name { get; set; }

        public int Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public int PatientId { get; set; }

        public int ExamType { get; set; }

        public int ExamStatus { get; set;}

        public string Comment { get; set; }
    }
}
