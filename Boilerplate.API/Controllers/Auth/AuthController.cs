using Boilerplate.API.Bases;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Auth.Setter.ForgetPassword;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : APIControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IHolderOfDTO holderOfDTO, IAuthService authService) : base(holderOfDTO)
        {
            _authService = authService;
        }

        /// <summary>
        /// User register in site 
        /// </summary>
        /// <returns> User register</returns>
        /// <remarks> User register in site</remarks>
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterSetterDTO userRegisterSetterDTO)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _authService.RegisterUserAsync(userRegisterSetterDTO));
        } 

        /// <summary>
        /// User can login in site or admin panal
        /// </summary>
        /// <returns> User login</returns>
        /// <remarks> User login in site or admin panal</remarks>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginSetterDTO userLoginSetterDTO)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            _holderOfDTO = await _authService.LoginAsync(userLoginSetterDTO);
            //CheckStateAndSetRefreshToken(HttpContext, _holderOfDTO);

            return State(_holderOfDTO);
        }

        /// <summary>
        /// User can login in admin panal
        /// </summary>
        /// <returns> User login</returns>
        /// <remarks> User login in admin panal</remarks>
        [AllowAnonymous]
        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdminAsync([FromBody] UserLoginSetterDTO userLoginSetterDTO)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            _holderOfDTO = await _authService.LoginAdminAsync(userLoginSetterDTO);
            // CheckStateAndSetRefreshToken(HttpContext, _holderOfDTO);

            return State(_holderOfDTO);
        }
         
         

    }
}
