using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryModel>> GetCountry();
        Task<CountryModel> GetCountryByIdAsync(string id);
        Task<string> AddCountryAsync(CountryModel country);
        Task<string> UpdateCountryAsync(string id, CountryModel country);
        Task<string> DeleteCountryAsync(string id);
        
    }
}