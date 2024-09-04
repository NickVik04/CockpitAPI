using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Interfaces;
using IdentityApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyModel>>> GetCurrency()
        {
            var currency = await _currencyService.GetCurrency();
            return Ok(currency);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyModel>> GetCurrencyById(string id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null){
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpPost]
        public async Task<ActionResult<CurrencyModel>> AddCurrency([FromBody] CurrencyModel model){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var currency = await _currencyService.AddCurrencyAsync(model);
            return Ok(currency);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrency(string id, [FromBody] CurrencyModel model){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var currency = await _currencyService.UpdateCurrencyAsync(id, model);
            if (currency == null){
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(string id){
            var currency = await _currencyService.DeleteCurrencyAsync(id);
            if(currency == null){
                return NotFound();
            }
            return NoContent();
        }
    }
}