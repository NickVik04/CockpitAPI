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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountry()
        {
            var countryList = await _countryService.GetCountry();
            return Ok(countryList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryModel>> GetCountryById(string id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult<CountryModel>> AddCountry([FromBody] CountryModel countryModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = await _countryService.AddCountryAsync(countryModel);
            return Ok(country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(string id, [FromBody] CountryModel countryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var country = await _countryService.UpdateCountryAsync(id, countryModel);

            if (country == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(string id)
        {
            var country = await _countryService.DeleteCountryAsync(id);

            if(country == null){
                return NotFound();
            }

            return NoContent();
        }

    }
}