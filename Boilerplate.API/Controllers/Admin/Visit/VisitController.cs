using Boilerplate.API.Bases;
using Boilerplate.Contracts.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Controllers.Admin.Visit
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class VisitController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public VisitController(HolderOfDTO holderOfDTO, IMediator mediator) : base(holderOfDTO)
        {
            _mediator = mediator;
        }
    }
}
