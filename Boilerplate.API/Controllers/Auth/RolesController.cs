using Boilerplate.API.Bases;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.Role;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleUser;
using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Auth;
using Boilerplate.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : APIControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IAuthService _authService;
        public RolesController(IHolderOfDTO holderOfDTO, IRoleService roleService, IAuthService authService) : base(holderOfDTO)
        {
            _roleService = roleService;
            _authService = authService;
        }

        /// <summary>
        /// Add or update role with permission in admin panal
        /// </summary>
        /// <remarks> 
        /// Add or update role with permission by user that has update permisssions
        /// </remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 2)]
        [HttpPost("UpdateRoleWithPermission")]
        public async Task<IActionResult> UpdateRoleWithPermissionAsync(RoleWithPermissionSetterDTO RoleSetterDTO)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.UpdateRoleWithPermissionAsync(RoleSetterDTO));
        }

        /// <summary>
        /// Add roles to user in admin panal
        /// </summary>
        /// <remarks> 
        /// Add roles to user by user that has update permisssions
        /// </remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 2)]
        [HttpPost("AddRolesToUser")]
        public async Task<IActionResult> AddRolesToUser(RoleUsersRequest UserRolesRequest)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.AddUserToRoleAsync(UserRolesRequest));
        }



        /// <summary>
        /// Get all  roles in admin panal 
        /// </summary>
        /// <returns> All  roles</returns>
        /// <remarks> Get all  roles without filter For user that has get permission</remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 1)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.GetAllAsync());
        }

        /// <summary>
        /// Get filtered roles in admin panal 
        /// </summary>
        /// <returns> filter roles</returns>
        /// <remarks> Get filtered roles For user that has get permission</remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 1)]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllAsync([FromQuery] RoleFilter roleFilter)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.SearchAsync(roleFilter));
        }

        /// <summary>
        /// Get all role with permissions in admin panal 
        /// </summary>
        /// <returns> All role with permissions</returns>
        /// <remarks> Get all roles with permissions For user that has get permission</remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 1)]
        [HttpGet("Permission")]
        public async Task<IActionResult> RoleWithPermissions()
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.RoleWithPermissions());
        }

        /// <summary>
        /// Get role in admin panal by Id
        /// </summary>
        /// <returns> Specific  role </returns>
        /// <remarks> Get Specific  role by id For user that has get permission</remarks>
        /// <param name="id" example="c1be5862-d402-4a31-8fs9-6aded859f7a8">The  role id</param>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.GetByIdAsync(id));
        }

        /// <summary>
        /// Add new role in admin panal
        /// </summary>
        /// <remarks> 
        /// Add role by user that has Add permisssions
        /// </remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 2)]
        [HttpPost]
        public async Task<IActionResult> PostAsync(RoleSetterDTO RoleSetterDTO)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.SaveAsync(RoleSetterDTO));
        }

        /// <summary>
        /// Update  role in admin panal
        /// </summary>
        /// <remarks> 
        /// Update role by user that has update permisssions
        /// </remarks>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 2)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RoleUpdateSetterDTO RoleSetterDTO)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.UpdateAsync(RoleSetterDTO));
        }

        /// <summary>
        /// Delete  role in admin panal
        /// </summary>
        /// <remarks> 
        /// Delete  role by user that has Delete permisssions
        /// </remarks>
        /// <param name="id" example="c1be5862-d402-4a31-8fs9-6aded859f7a8">The role id</param>
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 5)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _roleService.Delete(id));
        }


        /// <summary>
        /// Add role to user  role in admin panal
        /// </summary>
        /// <remarks> 
        ///Add role to user by user that has Add permisssions
        /// </remarks>
        /// <param name="id" example="c1be5862-d402-4a31-8fs9-6aded859f7a8">The role id</param>
        [Authorize]
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 3)]
        [HttpPost("addUserRole")]
        public async Task<IActionResult> AddUserRoleAsync([FromBody] RoleUserSetterDTO userRoleSetterDTO)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _authService.AddUserToRoleAsync(userRoleSetterDTO));
        }


        /// <summary>
        /// Get role with users in admin panal
        /// </summary>
        /// <remarks> 
        /// Delete Get role with users by user that has get permisssions
        /// </remarks>
        /// <param name="roleName" example="Admin">The role id</param>
        [Authorize]
        [ActionPermissionWithModuleAndOperation((long)Modules.Roles, 1)]
        [HttpPost("GetRoleUsers")]
        public async Task<IActionResult> GetRoleUsersAsync([FromBody] string roleName)
        {
            if (!ModelState.IsValid)
                return NotValidModelState();

            return State(await _authService.GetRoleUsersAsync(roleName));
        }

    }
}
