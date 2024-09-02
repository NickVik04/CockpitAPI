using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandModel>> GetBrands();
        Task<BrandModel> GetBrandByIdAsync(string id);
        Task<string> AddBrandAsync(BrandModel brand);
        Task<string> UpdateBrandAsync(string id, BrandModel brand);
        Task<string> DeleteBrandAsync(string id);
    }
}