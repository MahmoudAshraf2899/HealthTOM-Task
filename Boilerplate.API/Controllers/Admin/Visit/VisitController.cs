using Boilerplate.API.Bases;
using Boilerplate.Contracts.Features.Visit.Commands;
using Boilerplate.Contracts.Features.Visit.Queries;
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
        /// <summary>
        /// This End Point To Create New Visit By Radiologists 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] VisitAddCommand command)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();


            return State(await _mediator.Send(command));
        }
        /// <summary>
        /// Get All Visits included pagination and filteration with patient name 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllVisitsQuery command)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();


            return State(await _mediator.Send(command));
        }
    }
}
