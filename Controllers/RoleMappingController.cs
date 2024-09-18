using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Interfaces;
using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleMappingController : ControllerBase
    {
       private readonly IRoleMappingService _roleMappingService;

        public RoleMappingController(IRoleMappingService RoleMappingService)
        {
            _roleMappingService = RoleMappingService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleMappingModel>>> GetRoles()
        {
            var Roles = await _roleMappingService.GetRoleMappings();
            return Ok(Roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleMappingModel>> GetRoleById(string id)
        {
            var Role = await _roleMappingService.GetRoleMappingByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }
            return Ok(Role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleMappingModel>> AddRole([FromBody] RoleMappingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedRole = await _roleMappingService.AddRoleMappingAsync(model);
            return Ok(addedRole);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleMappingModel RoleMappingModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var updatedRole = await _roleMappingService.UpdateRoleMappingAsync(id, RoleMappingModel);
        //     if (updatedRole == null)
        //     {
        //         return NotFound();
        //     }
        //     return NoContent();
        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var deletedRole = await _roleMappingService.DeleteRoleMappingAsync(id);
            if(deletedRole == null){
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        [Route("GetUserRoleMapping")]
        public async Task<ActionResult<RoleMappingModel>> GetUserRoleMapping(){
            var getUserRole = await _roleMappingService.GetUserRoleMapping();
            if(getUserRole == null){
                return NotFound();
            }
            return Ok(getUserRole);
        }
    }
}