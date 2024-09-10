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
    public class LanguageController : ControllerBase
    {
       private readonly ILanguageService _languageService;

       public LanguageController(ILanguageService languageService)
       {
        _languageService = languageService;
        
       }

       [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageModel>>> GetLanguage()
        {
            var languageList = await _languageService.GetLanguage();
            return Ok(languageList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageModel>> GetLanguageById(string id)
        {
            var language = await _languageService.GetLanguageByIdAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        [HttpPost]
        public async Task<ActionResult<LanguageModel>> AddLanguage([FromBody] LanguageModel languageModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var language = await _languageService.AddLanguageAsync(languageModel);
            return Ok(language);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguage(string id, [FromBody] LanguageModel languageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var language = await _languageService.UpdateLanguageAsync(id, languageModel);

            if (language == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(string id)
        {
            var language = await _languageService.DeleteLanguageAsync(id);

            if(language == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}