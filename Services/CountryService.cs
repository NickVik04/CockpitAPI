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
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CountryService> _logger;

        public CountryService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<CountryService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }
        public async Task<string> AddCountryAsync(CountryModel country)
        {
            try
            {

                var userId = GetCurrentUserId();
                country.UpdatedBy = userId;
                country.CreatedBy = userId;

                _context.Country.Add(country);
                await _context.SaveChangesAsync();
                return "Success";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new country.");
                return "Failure: " + ex.Message;
            }
        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }


        public async Task<string> DeleteCountryAsync(string id)
        {
            try{
            var country =  await _context.Country.FindAsync(id);

            if(country == null){
                return "Not Found";
            }

            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return "Success";
        }

        catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the country with ID: {id}.");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<IEnumerable<CountryModel>> GetCountry()
        {
            try{
                var country =  _context.Country.ToList();
                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching country.");
                return Enumerable.Empty<CountryModel>();
            }
        }

        public async Task<CountryModel> GetCountryByIdAsync(string id)
        {
            try{
                var country = await _context.Country.FindAsync(id);

                if(country == null){
                    return null;
                }

                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching country.");
                return null;
            }
        }

        public async Task<string> UpdateCountryAsync(string id, CountryModel countryModel)
        {
          try{
                var userId = GetCurrentUserId();

                var country = await _context.Country.FindAsync(id);
                if(country == null){
                    return "No Country";
                }

                country.UpdatedBy = userId;
                country.Country = countryModel.Country;
                country.Currency = countryModel.Currency;
                country.Language = countryModel.Language;

                _context.Country.Update(country);
                await _context.SaveChangesAsync();
                return "Success";

          }
          catch (Exception ex){

            _logger.LogError(ex, $"An error occured while updating country with ID: {id}");
            return "Failure: " + ex.Message;

          }
        }
    }
}