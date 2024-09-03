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
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<StoreService> _logger;

        // Constructor with ILogger injected
        public StoreService(ApplicationDbContext context, ILogger<StoreService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<StoreModel>> GetStores()
        {
            try
            {
                var store = _context.Stores.ToList();
                return store;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching store.");
                return Enumerable.Empty<StoreModel>();
            }
        }

        public async Task<StoreModel> GetStoreByIdAsync(string id)
        {
            try
            {
                var store = await _context.Stores.FindAsync(id);
                if (store == null)
                {
                    return null;
                }
                return store;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching store with ID: {id}");
                return null;
            }
        }

        public async Task<string> AddStoreAsync(StoreModel store)
        {
            try
            {
                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                store.CreatedBy = userId;
                store.UpdatedBy = userId;

                _context.Stores.Add(store);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new store.");
                return "Failure: " + ex.Message;
            }
        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

        public async Task<string> UpdateStoreAsync(string id, StoreModel StoreModel)
        {
            try
            {
                var Store = await _context.Stores.FindAsync(id);

                if (Store == null)
                {
                    return "No Store";
                }

                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                Store.UpdatedBy = userId;


                // Update Store properties
                Store.Name = StoreModel.Name;
                Store.Logo = StoreModel.Logo;
                Store.Code = StoreModel.Code;
                Store.AlternateCode = StoreModel.AlternateCode;
                Store.Type = StoreModel.Type;
                Store.Url = StoreModel.Url;
                Store.GSTIN = StoreModel.GSTIN;
                Store.EmailID = StoreModel.EmailID;
                Store.Address1 = StoreModel.Address1;
                Store.Address2 = StoreModel.Address2;
                Store.Latitude = StoreModel.Latitude;
                Store.Longitude = StoreModel.Longitude;
                Store.Country = StoreModel.Country;
                Store.State = StoreModel.State;
                Store.City = StoreModel.City;
                Store.Zone = StoreModel.Zone;
                Store.Region = StoreModel.Region;
                Store.Pincode = StoreModel.Pincode;
                Store.Timing = StoreModel.Timing;
                Store.CIN = StoreModel.CIN;
                Store.PAN = StoreModel.PAN;

                _context.Stores.Update(Store);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the Store with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<string> DeleteStoreAsync(string id)
        {
            try
            {
                var Store = await _context.Stores.FindAsync(id);
                if (Store == null)
                {
                    return "No Store";
                }

                _context.Stores.Remove(Store);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the Store with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }
    }
}