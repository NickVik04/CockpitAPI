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
    public class LanguageService : ILanguageService
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<LanguageService> _logger;

        public LanguageService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<LanguageService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<string> AddLanguageAsync(LanguageModel language)
        {
            try
            {

                var userId = GetCurrentUserId();
                language.UpdatedBy = userId;
                language.CreatedBy = userId;

                _context.Language.Add(language);
                await _context.SaveChangesAsync();
                return "Success";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new language.");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<string> DeleteLanguageAsync(string id)
        {
            try
            {
                var language = await _context.Language.FindAsync(id);

                if (language == null)
                {
                    return "Not Found";
                }

                _context.Language.Remove(language);
                await _context.SaveChangesAsync();
                return "Success";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the language with ID: {id}.");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<IEnumerable<LanguageModel>> GetLanguage()
        {
            try
            {
                var language = _context.Language.ToList();
                return language;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching language.");
                return Enumerable.Empty<LanguageModel>();
            }
        }

        public async Task<LanguageModel> GetLanguageByIdAsync(string id)
        {
            try
            {
                var language = await _context.Language.FindAsync(id);

                if (language == null)
                {
                    return null;
                }

                return language;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching language.");
                return null;
            }
        }


        public async Task<string> UpdateLanguageAsync(string id, LanguageModel languageModel)
        {
            try
            {
                var userId = GetCurrentUserId();

                var language = await _context.Language.FindAsync(id);
                if (language == null)
                {
                    return "No Language";
                }

                language.UpdatedBy = userId;
                language.Language = languageModel.Language;
                language.Symbol = languageModel.Symbol;
                language.Brand = languageModel.Brand;
                language.Store = languageModel.Store;

                _context.Language.Update(language);
                await _context.SaveChangesAsync();
                return "Success";

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"An error occured while updating Language with ID: {id}");
                return "Failure: " + ex.Message;

            }

        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

    }
}