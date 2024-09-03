using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Data;
using IdentityApi.Interfaces;
using IdentityApi.Models;
using Microsoft.Extensions.Logging;

namespace IdentityApi.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<BrandService> _logger;

        // Constructor with ILogger injected
        public BrandService(ApplicationDbContext context, ILogger<BrandService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<BrandModel>> GetBrands()
        {
            try
            {
                var brands =  _context.Brands.ToList();
                return brands;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching brands.");
                return Enumerable.Empty<BrandModel>(); // Return an empty list if there's an error
            }
        }

        public async Task<BrandModel> GetBrandByIdAsync(string id)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    return null;
                }

                return brand;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the brand with ID: {id}");
                return null; // Return null if there's an error
            }
        }

        public async Task<string> AddBrandAsync(BrandModel brand)
        {
            try
            {
                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                brand.CreatedBy = userId;
                brand.UpdatedBy = userId;
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new brand.");
                return "Failure: " + ex.Message;
            }
        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

        public async Task<string> UpdateBrandAsync(string id, BrandModel brandModel)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);

                if (brand == null)
                {
                    return "No Brand";
                }
                var userId = GetCurrentUserId();
                
                brand.UpdatedBy = userId;
                // Update brand properties
                brand.Name = brandModel.Name;
                brand.Logo = brandModel.Logo;
                brand.Stores = brandModel.Stores;
                brand.Code = brandModel.Code;
                brand.Url = brandModel.Url;
                brand.SMSSenderID = brandModel.SMSSenderID;
                brand.EmailSenderID = brandModel.EmailSenderID;
                brand.SenderEmailAddress = brandModel.SenderEmailAddress;
                brand.CIN = brandModel.CIN;

                _context.Brands.Update(brand);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the brand with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<string> DeleteBrandAsync(string id)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    return "No Brand";
                }

                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the brand with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }
    }
}
