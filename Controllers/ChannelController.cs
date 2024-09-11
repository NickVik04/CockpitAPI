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
    public class ChannelController : ControllerBase
    {
       private readonly IChannelService _channelService;

       public ChannelController(IChannelService channelService)
       {
        _channelService = channelService;        
       }

       [HttpGet]
        public async Task<ActionResult<IEnumerable<ChannelModel>>> GetChannels()
        {
            var Channels = await _channelService.GetChannels();
            return Ok(Channels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChannelModel>> GetChannelById(string id)
        {
            var Channel = await _channelService.GetChannelByIdAsync(id);
            if (Channel == null)
            {
                return NotFound();
            }
            return Ok(Channel);
        }

        [HttpPost]
        public async Task<ActionResult<ChannelModel>> AddChannel([FromBody] ChannelModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedChannel = await _channelService.AddChannelAsync(model);
            return Ok(addedChannel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChannel(string id, [FromBody] ChannelModel ChannelModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedChannel = await _channelService.UpdateChannelAsync(id, ChannelModel);
            if (updatedChannel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel(string id)
        {
            var deletedChannel = await _channelService.DeleteChannelAsync(id);
            if(deletedChannel == null){
                return NotFound();
            }
            return NoContent();
        }
    }
}