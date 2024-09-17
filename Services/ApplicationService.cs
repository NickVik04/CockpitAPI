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
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ApplicationService> _logger;

        // Constructor with ILogger injected
        public ApplicationService(ApplicationDbContext context, ILogger<ApplicationService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddApplicationAsync(ApplicationModel application)
        {
            try
            {
                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                application.CreatedBy = userId;
                application.UpdatedBy = userId;
                _context.Application.Add(application);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new application.");
                return "Failure: " + ex.Message;
            }
        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

        public async Task<string> DeleteApplicationAsync(string id)
        {
           try
            {
                var application = await _context.Application.FindAsync(id);
                if (application == null)
                {
                    return "No application";
                }

                _context.Application.Remove(application);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the application with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<IEnumerable<ApplicationModel>> GetApplication()
        {
            try
            {
                var application =  _context.Application.ToList();
                return application;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching application.");
                return Enumerable.Empty<ApplicationModel>(); // Return an empty list if there's an error
            }
        }

        public async Task<ApplicationModel> GetApplicationByIdAsync(string id)
        {
            try
            {
                var application = await _context.Application.FindAsync(id);
                if (application == null)
                {
                    return null;
                }

                return application;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the application with ID: {id}");
                return null; // Return null if there's an error
            }
        }

        public async Task<string> UpdateApplicationAsync(string id, ApplicationModel applicationModel)
        {
            try
            {
                var application = await _context.Application.FindAsync(id);

                if (application == null)
                {
                    return "No Application";
                }
                var userId = GetCurrentUserId();
                
                application.UpdatedBy = userId;
                // Update brand properties
                application.ApplicationName = applicationModel.ApplicationName;

                _context.Application.Update(application);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the application with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }
    }
}