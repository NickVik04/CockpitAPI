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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreModel>>> GetStores()
        {
            var stores = await _storeService.GetStores();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreModel>> GetStoreById(string id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        [HttpPost]
        public async Task<ActionResult<StoreModel>> AddStore([FromBody] StoreModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var store = await _storeService.AddStoreAsync(model);
            return Ok(store);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(string id, [FromBody] StoreModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var store = await _storeService.UpdateStoreAsync(id, model);
            if (store == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(string id) {
            var store = await _storeService.DeleteStoreAsync(id);
            if(store == null){
                return NotFound();
            }
            return NoContent();
        }
    }
}