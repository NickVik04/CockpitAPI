using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Data;
using IdentityApi.Interfaces;
using IdentityApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Services
{
    public class RoleMappingService : IRoleMappingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RoleMappingService> _logger;

        // Constructor with ILogger injected
        public RoleMappingService(ApplicationDbContext context, ILogger<RoleMappingService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        // public async Task<string> AddRoleMappingAsync(RoleMappingModel roleMapping)
        // {
        //     try
        //     {

        //         var userIds = roleMapping.UserID.Split(",");


        //         // Get all existing users from the database
        //         var existingUsers = await _context.RoleMapping
        //             .Where(x => userIds.Contains(x.UserID))
        //             .ToListAsync();

        //         // Split the logic for existing and new users
        //         var newUsers = userIds.Except(existingUsers.Select(x => x.UserID)).ToList(); // New users (not in DB)
        //         var usersToUpdate = existingUsers; // Existing users to be updated

        //         if (emp != null)
        //         {
        //             foreach (var user in usersToUpdate)
        //             {
        //                 user.ApplicationID = roleMapping.ApplicationID;
        //                 user.RoleID = roleMapping.RoleID;
        //                 user.UpdatedBy = GetCurrentUserId();  // Set the updatedBy value
        //                 _context.RoleMapping.Update(user); // Mark user for update
        //             }

        //             // Add new users
        //             foreach (var newUserId in newUsers)
        //             {
        //                 var newUser = new RoleMappingModel()
        //                 {
        //                     UserID = newUserId,
        //                     ApplicationID = roleMapping.ApplicationID,
        //                     RoleID = roleMapping.RoleID,
        //                     CreatedBy = GetCurrentUserId(),
        //                     UpdatedBy = GetCurrentUserId(),
        //                 };
        //                 await _context.RoleMapping.AddAsync(newUser);
        //             }

        //             // updated
        //         }
        //         else
        //         {
        //             //add

        //             var userId = GetCurrentUserId();

        //             var roleMappings = userIds.Select(userId => new RoleMappingModel
        //             {
        //                 UserID = userId,
        //                 ApplicationID = roleMapping.ApplicationID,
        //                 RoleID = roleMapping.RoleID,
        //                 CreatedBy = userId,
        //                 UpdatedBy = userId,

        //             }).ToList();

        //             await _context.RoleMapping.AddRangeAsync(roleMappings);
        //             await _context.SaveChangesAsync();
        //         }







        //         return "Success";
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "An error occurred while adding new Role Mappings.");
        //         return "Failure: " + ex.Message;
        //     }
        // }

        public async Task<string> AddRoleMappingAsync(RoleMappingModel roleMapping)
        {
            try
            {
                var userIds = roleMapping.UserID?.Split(",") ?? Array.Empty<string>();

                if (userIds.Length == 0)
                {
                    return "Failure: No user IDs provided.";
                }

                // Get all existing users from the database
                var existingUsers = await _context.RoleMapping
                    .Where(x => userIds.Contains(x.UserID))
                    .ToListAsync();

                // Split the logic for existing and new users
                var newUsers = userIds.Except(existingUsers.Select(x => x.UserID)).ToList(); // New users (not in DB)
                var usersToUpdate = existingUsers; // Existing users to be updated

                // Update existing users
                if (usersToUpdate.Any())
                {
                    foreach (var user in usersToUpdate)
                    {
                        user.ApplicationID = roleMapping.ApplicationID;
                        user.RoleID = roleMapping.RoleID;
                        user.UpdatedBy = GetCurrentUserId();  // Set the updatedBy value
                    }

                    // Update in bulk
                    _context.RoleMapping.UpdateRange(usersToUpdate);
                }

                // Add new users
                if (newUsers.Any())
                {
                    var currentUserId = GetCurrentUserId();
                    var newRoleMappings = newUsers.Select(newUserId => new RoleMappingModel
                    {
                        UserID = newUserId,
                        ApplicationID = roleMapping.ApplicationID,
                        RoleID = roleMapping.RoleID,
                        CreatedBy = currentUserId,
                        UpdatedBy = currentUserId,
                    }).ToList();

                    await _context.RoleMapping.AddRangeAsync(newRoleMappings);  // Add in bulk
                }

                // Save changes
                await _context.SaveChangesAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding/updating Role Mapping.");
                return "Failure: " + ex.Message;
            }
        }

        private string GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdString ?? string.Empty;  // Return an empty string if userIdString is null
        }

        public async Task<string> DeleteRoleMappingAsync(string id)
        {
            try
            {
                var roleMapping = await _context.RoleMapping.FindAsync(id);
                if (roleMapping == null)
                {
                    return "No Role Mapping";
                }

                _context.RoleMapping.Remove(roleMapping);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the Role Mapping with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<RoleMappingModel> GetRoleMappingByIdAsync(string id)
        {
            try
            {
                var roleMapping = await _context.RoleMapping.FindAsync(id);
                if (roleMapping == null)
                {
                    return null;
                }

                return roleMapping;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the Role Mapping with ID: {id}");
                return null; // Return null if there's an error
            }
        }

        public async Task<IEnumerable<RoleMappingModel>> GetRoleMappings()
        {
            try
            {
                var roleMapping = _context.RoleMapping.ToList();
                return roleMapping;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the Role Mapping.");
                return Enumerable.Empty<RoleMappingModel>(); // Return an empty list if there's an error
            }
        }

        // public async Task<string> UpdateRoleMappingAsync(string id, RoleMappingModel roleMappingModel)
        // {
        //     try
        //     {
        //         var roleMapping = await _context.RoleMapping.FindAsync(id);

        //         if (roleMapping == null)
        //         {
        //             return "No Role Mapping";
        //         }
        //         var userId = GetCurrentUserId();

        //         roleMapping.UpdatedBy = userId;
        //         // Update roleMapping properties
        //         roleMapping.ApplicationID = roleMappingModel.ApplicationID;
        //         roleMapping.UserID = roleMappingModel.UserID;
        //         roleMapping.RoleID = roleMappingModel.RoleID;

        //         _context.RoleMapping.Update(roleMapping);
        //         await _context.SaveChangesAsync();
        //         return "Success";
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, $"An error occurred while updating the Role Mapping with ID: {id}");
        //         return "Failure: " + ex.Message;
        //     }
        // }
        public async Task<List<UserRoleMappingDetailModel>> GetUserRoleMapping()
        {
            try
            {
                // Fetch all necessary data
                var roleMappings = await _context.RoleMapping.ToListAsync();
                var users = await _context.Users.ToListAsync();
                var applications = await _context.Application.ToListAsync();
                var roles = await _context.Role.ToListAsync();

                var result = from trns in roleMappings
                             join u in users on trns.UserID equals u.Id
                             join st in applications on trns.ApplicationID equals st.Id
                             select new
                             {
                                 u.UserName,
                                 u.EmailId,
                                 u.PhoneNo,
                                 u.Status,
                                 st.ApplicationName,
                                 trns.RoleID
                             };

                var details = new List<UserRoleMappingDetailModel>();

                foreach (var item in result)
                {
                    // Split the comma-separated RoleID
                    var roleIds = item.RoleID.Split(',').ToList();

                    var roleNames = roles
                        .Where(r => roleIds.Contains(r.Id))
                        .Select(r => r.RoleName)
                        .ToList();

                    var userRoleDetail = new UserRoleMappingDetailModel
                    {
                        UserName = item.UserName,
                        UserEmail = item.EmailId,
                        ContactNumber = item.PhoneNo,
                        CockpitRole = string.Join(", ", roleNames),
                        Applications = item.ApplicationName,
                        Status = item.Status
                    };

                    details.Add(userRoleDetail);
                }

                return details;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Fetching the Role Mappings");
                return null;
            }
        }

    }
}