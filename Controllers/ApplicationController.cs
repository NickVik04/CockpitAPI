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
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService){
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetApplications()
        {
            var Applications = await _applicationService.GetApplication();
            return Ok(Applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationModel>> GetApplicationById(string id)
        {
            var Application = await _applicationService.GetApplicationByIdAsync(id);
            if (Application == null)
            {
                return NotFound();
            }
            return Ok(Application);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationModel>> AddApplication([FromBody] ApplicationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedApplication = await _applicationService.AddApplicationAsync(model);
            return Ok(addedApplication);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(string id, [FromBody] ApplicationModel ApplicationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedApplication = await _applicationService.UpdateApplicationAsync(id, ApplicationModel);
            if (updatedApplication == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(string id)
        {
            var deletedApplication = await _applicationService.DeleteApplicationAsync(id);
            if(deletedApplication == null){
                return NotFound();
            }
            return NoContent();
        }

    }
}