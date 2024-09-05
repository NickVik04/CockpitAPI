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
    public class CurrencyService : ICurrencyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CurrencyService> _logger;
        public CurrencyService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<CurrencyService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async Task<IEnumerable<CurrencyModel>> GetCurrency()
        {
            try
            {
                var currency = _context.Currency.ToList();
                return currency;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching currency.");
                return Enumerable.Empty<CurrencyModel>();
            }
        }

        public async Task<CurrencyModel> GetCurrencyByIdAsync(string id)
        {
            try
            {
                var currency = await _context.Currency.FindAsync(id);
                if (currency == null)
                {
                    return null;
                }
                return currency;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching currency with ID: {id}");
                return null;
            }
        }

        public async Task<string> AddCurrencyAsync(CurrencyModel currency)
        {
            try
            {
                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                currency.CreatedBy = userId;
                currency.UpdatedBy = userId;

                _context.Currency.Add(currency);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new currency.");
                return "Failure: " + ex.Message;
            }
        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

        public async Task<string> UpdateCurrencyAsync(string id, CurrencyModel currencyModel)
        {
            try
            {
                var currency = await _context.Currency.FindAsync(id);

                if (currency == null)
                {
                    return "No Currency";
                }

                var userId = GetCurrentUserId();
                currency.UpdatedBy = userId;

                currency.Name = currencyModel.Name;
                currency.Symbol = currencyModel.Symbol;
                currency.Decimal = currencyModel.Decimal;
                currency.Brand = currencyModel.Brand;
                currency.Store = currencyModel.Store;
                currency.Code = currencyModel.Code;

                _context.Currency.Update(currency);
                await _context.SaveChangesAsync();
                return "Success";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the Currency with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<string> DeleteCurrencyAsync(string id)
        {
            try{
                var currency = await _context.Currency.FindAsync(id);
                if(currency == null){
                    return "No Currency";
                }

                _context.Currency.Remove(currency);
                await _context.SaveChangesAsync();
                return "Success";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the Currency with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }


    }
}