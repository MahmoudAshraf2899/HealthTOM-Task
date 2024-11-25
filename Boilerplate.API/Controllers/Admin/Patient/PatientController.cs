using Boilerplate.API.Bases;
using Boilerplate.Contracts.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Controllers.Admin.Patient
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PatientController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(HolderOfDTO holderOfDTO, IMediator mediator) : base(holderOfDTO)
        {
            _mediator = mediator;
        }
    }
}
