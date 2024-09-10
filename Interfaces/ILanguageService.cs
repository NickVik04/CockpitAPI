using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageModel>> GetLanguage();
        Task<LanguageModel> GetLanguageByIdAsync(string id);
        Task<string> AddLanguageAsync(LanguageModel Language);
        Task<string> UpdateLanguageAsync(string id, LanguageModel Language);
        Task<string> DeleteLanguageAsync(string id);
    }
}