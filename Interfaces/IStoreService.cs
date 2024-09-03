using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreModel>> GetStores();
        Task<StoreModel> GetStoreByIdAsync(string id);
        Task<string> AddStoreAsync(StoreModel store);
        Task<string> UpdateStoreAsync(string id, StoreModel store);
        Task<string> DeleteStoreAsync(string id);
    }
}