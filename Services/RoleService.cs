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
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RoleService> _logger;

        // Constructor with ILogger injected
        public RoleService(ApplicationDbContext context, ILogger<RoleService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> AddRoleAsync(RoleModel role)
        {
            try
            {
                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                role.CreatedBy = userId;
                role.UpdatedBy = userId;
                _context.Role.Add(role);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new Role.");
                return "Failure: " + ex.Message;
            }
        }
        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

        public async Task<string> DeleteRoleAsync(string id)
        {
             try
            {
                var role = await _context.Role.FindAsync(id);
                if (role == null)
                {
                    return "No Role";
                }

                _context.Role.Remove(role);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the role with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<RoleModel> GetRoleByIdAsync(string id)
        {
             try
            {
                var role = await _context.Role.FindAsync(id);
                if (role == null)
                {
                    return null;
                }

                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the role with ID: {id}");
                return null; // Return null if there's an error
            }
        }

        public async Task<IEnumerable<RoleModel>> GetRoles()
        {
            try
            {
                var role =  _context.Role.ToList();
                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching roles.");
                return Enumerable.Empty<RoleModel>(); // Return an empty list if there's an error
            }
        }

        public async Task<string> UpdateRoleAsync(string id, RoleModel roleModel)
        {
            try
            {
                var role = await _context.Role.FindAsync(id);

                if (role == null)
                {
                    return "No Role";
                }
                var userId = GetCurrentUserId();
                
                role.UpdatedBy = userId;
                // Update role properties
                role.RoleName = roleModel.RoleName;

                _context.Role.Update(role);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the role with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }
    }
}