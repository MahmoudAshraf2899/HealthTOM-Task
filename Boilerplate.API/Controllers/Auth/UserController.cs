using Boilerplate.API.Bases;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Auth;
using Boilerplate.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : APIControllerBase
    {
        private readonly IUserService _userService;
        private DataHub hub = new DataHub();

        public UserController(IHolderOfDTO holderOfDTO, IUserService userService) : base(holderOfDTO)
        {
            _userService = userService;
        }
         
        
        /// <summary>
        /// Search users in admin panal 
        /// </summary>
        /// <returns> Search  users</returns>
        /// <remarks> filter users by user that has get permission</remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Users, 1)]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchAsync([FromQuery] UserFilter filter)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();
            return State(await _userService.SearchAsync(filter));
        }

         

        /// <summary>
        /// Get  user in admin panal by Id
        /// </summary>
        /// <returns> Specific  user </returns>
        /// <remarks> Get Specific user by id for user that has get permission</remarks>
        /// <param name="id" example="c1be5862-d402-4a31-8fs9-6aded859f7a8">The  user id</param>
        [ActionPermissionWithModuleAndOperation((long)Modules.Users, 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();
            return State(await _userService.GetByIdAsync(id));
        }          
    }
}
