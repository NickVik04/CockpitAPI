using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Data;
using IdentityApi.Interfaces;
using IdentityApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Services
{
    public class UserService : IUserService
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
        {
            //_userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = _context.Users.ToList();
            // var userModels = users.Select(user => new UserModel
            // {
            //     Id = user.Id,
            //     UserName = user.UserName,
            //     EmailId = user.EmailId,
            //     Address1 = user.Address1,
            //     Address2 = user.Address2,
            //     PhoneNo = user.PhoneNo,
            //     Country = user.Country,
            //     State = user.State,
            //     Pincode = user.Pincode,
            //     Brand = user.Brand,
            //     Store = user.Store,
            //     Status = user.Status,
            //     // Map other properties as needed
            // });

            return users;
        }

        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null; // Or throw an exception if preferred
            }

            return user;
        }

        public async Task<string> AddUserAsync(UserModel userModel)
        {
            try
            {
                var userId = GetCurrentUserId();

                // Set CreatedBy and UpdatedBy fields
                userModel.CreatedBy = userId;
                userModel.UpdatedBy = userId;
                _context.Users.Add(userModel);
                await _context.SaveChangesAsync();
                _context.SaveChanges();
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

        public async Task<string> UpdateUserAsync(string id, UserModel userModel)
        {
            try
            {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return "No User"; // Or throw an exception if preferred
            }

            var userId = GetCurrentUserId();
            userModel.UpdatedBy = userId;

            // Update user properties
            user.UserName = userModel.UserName;
            user.EmailId = userModel.EmailId;

            user.Address1 = user.Address1;
            user.Address2 = user.Address2;
            user.PhoneNo = user.PhoneNo;
            user.Country = user.Country;
            user.State = user.State;
            user.Pincode = user.Pincode;
            user.Brand = user.Brand;
            user.Store = user.Store;
            user.Status = user.Status;
            // Map other properties as needed

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the Store with ID: {id}");
                return "Failure: " + ex.Message;
            }
        }

        public async Task<string> DeleteUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return "No User"; // Or throw an exception if preferred
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                // Log exception
                return "Failure: " + ex.Message;
            }
        }
    }
}