using AutoMapper;
using Boilerplate.Contracts.Features.Visit.Queries;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.Interfaces.Services.Visit;
using Boilerplate.Core.Bases;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Visit.Queries
{
    public class GetAllVisitsQueryHandler : BaseService<GetAllVisitsQueryHandler>, IRequestHandler<GetAllVisitsQuery, IHolderOfDTO>
    {
        private readonly IVisitService _visitService;

        public GetAllVisitsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture, IVisitService visitService,
            ILogger<GetAllVisitsQueryHandler> logger, IHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
            _visitService = visitService;
        }

        public async Task<IHolderOfDTO> Handle(GetAllVisitsQuery request, CancellationToken cancellationToken)
        {
            return await _visitService.GetAllAsync(request);
        }
    }
}
