using Boilerplate.API.Bases;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Lookups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Controllers.Admin.Lookups
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LookupController : APIControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupController(IHolderOfDTO holderOfDTO, ILookupService lookupService) : base(holderOfDTO)
        {
            _lookupService = lookupService;
        }


        /// <summary>
        /// Get User lookups in admin panal 
        /// </summary>
        /// <returns> Get User lookups in admin panal</returns>
        /// <remarks> Get User lookups in admin panal</remarks>
        [HttpGet("Users")]
        public async Task<IActionResult> GetUserAsync()
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _lookupService.GetUserAsync());
        }

        /// <summary>
        /// Get Role lookups in admin panal 
        /// </summary>
        /// <returns> Get Role lookups in admin panal</returns>
        /// <remarks> Get Role lookups in admin panal</remarks>
        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoleAsync()
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _lookupService.GetRoleAsync());
        } 
    }
}
