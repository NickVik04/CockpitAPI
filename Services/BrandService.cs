using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ILogger<BrandService> _logger;

        // Constructor with ILogger injected
        public BrandService(ApplicationDbContext context, ILogger<BrandService> logger)
        {
            _context = context;
            _logger = logger;
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

                return new BrandModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Logo = brand.Logo,
                    Stores = brand.Stores,
                    Code = brand.Code,
                    Url = brand.Url,
                    SMSSenderID = brand.SMSSenderID,
                    EmailSenderID = brand.EmailSenderID,
                    SenderEmailAddress = brand.SenderEmailAddress,
                    CIN = brand.CIN
                };
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

        public async Task<string> UpdateBrandAsync(string id, BrandModel brandModel)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(brandModel.Id);

                if (brand == null)
                {
                    return "No Brand";
                }

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
