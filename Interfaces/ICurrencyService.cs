using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyModel>> GetCurrency();
        Task<CurrencyModel> GetCurrencyByIdAsync(string id);
        Task<string> AddCurrencyAsync(CurrencyModel Currency);
        Task<string> UpdateCurrencyAsync(string id, CurrencyModel Currency);
        Task<string> DeleteCurrencyAsync(string id);

    }
}