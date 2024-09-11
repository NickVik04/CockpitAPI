using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface IChannelService
    {
        Task<IEnumerable<ChannelModel>> GetChannels();
        Task<ChannelModel> GetChannelByIdAsync(string id);
        Task<string> AddChannelAsync(ChannelModel channel);
        Task<string> UpdateChannelAsync(string id, ChannelModel channel);
        Task<string> DeleteChannelAsync(string id);
        
    }
}