using Boilerplate.Contracts.Features.Visit.Commands;
using Boilerplate.Core.Entities.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Helpers
{

    public partial class MappingProfile
    {
        private void VisitMappingProfile()
        {
            CreateMap<VisitAddCommand, Visit>().ReverseMap();

        }
    }
}
