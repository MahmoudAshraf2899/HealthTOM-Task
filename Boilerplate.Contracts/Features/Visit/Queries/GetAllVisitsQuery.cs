using Boilerplate.Contracts.Filters;
using Boilerplate.Contracts.Interfaces.Custom;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Contracts.Features.Visit.Queries
{
    public class GetAllVisitsQuery : VisitsFilter, IRequest<IHolderOfDTO>
    {
    }
}
