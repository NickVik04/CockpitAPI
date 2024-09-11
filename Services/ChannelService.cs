using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Data;
using IdentityApi.Interfaces;
using IdentityApi.Models;

namespace IdentityApi.Services
{
    public class ChannelService : IChannelService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ChannelService> _logger;

        public ChannelService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ChannelService> logger){
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        } 
        public async Task<string> AddChannelAsync(ChannelModel channel)
        {
            try
            {
                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                channel.CreatedBy = userId;
                channel.UpdatedBy = userId;
                _context.Channel.Add(channel);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new channel.");
                return "Failure: " + ex.Message;
            }
        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

        public async Task<string> DeleteChannelAsync(string id)
        {
            try
            {
                var channel = await _context.Channel.FindAsync(id);
                if (channel == null)
                {
                    return "No Channel";
                }

                _context.Channel.Remove(channel);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the channel with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<ChannelModel> GetChannelByIdAsync(string id)
        {
            try
            {
                var channel = await _context.Channel.FindAsync(id);
                if (channel == null)
                {
                    return null;
                }

                return channel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the channel with ID: {id}");
                return null; // Return null if there's an error
            }
        }

        public async Task<IEnumerable<ChannelModel>> GetChannels()
        {
            try
            {
                var channel =  _context.Channel.ToList();
                return channel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching channels.");
                return Enumerable.Empty<ChannelModel>(); // Return an empty list if there's an error
            }
        }

        public async Task<string> UpdateChannelAsync(string id, ChannelModel channelModel)
        {
            try
            {
                var channel = await _context.Channel.FindAsync(id);

                if (channel == null)
                {
                    return "No Channel";
                }
                var userId = GetCurrentUserId();
                
                channel.UpdatedBy = userId;
                // Update channel properties
                channel.Name = channelModel.Name;
                channel.ChannelCode = channelModel.ChannelCode;

                _context.Channel.Update(channel);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the channel with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }
    }
}