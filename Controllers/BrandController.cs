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
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandModel>>> GetBrands()
        {
            var brands = await _brandService.GetBrands();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandModel>> GetBrandById(string id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<BrandModel>> AddBrand([FromBody] BrandModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedBrand = await _brandService.AddBrandAsync(model);
            return Ok(addedBrand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(string id, [FromBody] BrandModel brandModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedBrand = await _brandService.UpdateBrandAsync(id, brandModel);
            if (updatedBrand == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            var deletedBrand = await _brandService.DeleteBrandAsync(id);
            if(deletedBrand == null){
                return NotFound();
            }
            return NoContent();
        }

    }
}