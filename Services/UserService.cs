using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserService(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            //_userManager = userManager;
            _context = context;
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

            return new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                EmailId = user.EmailId,
                Address1 = user.Address1,
                Address2 = user.Address2,
                PhoneNo = user.PhoneNo,
                Country = user.Country,
                State = user.State,
                Pincode = user.Pincode,
                Brand = user.Brand,
                Store = user.Store,
                Status = user.Status,
                // Map other properties as needed
            };
        }

        public async Task<string> AddUserAsync(UserModel userModel)
        {
            var result = _context.Users.Add(userModel);
           await _context.SaveChangesAsync();
            int id = _context.SaveChanges();

            if (id > 0)
            {
                return "Failure";
            }
            else
            {
                return "Success";
            }
        }

        public async Task<string> UpdateUserAsync(string id, UserModel userModel)
        {
            var user = await _context.Users.FindAsync(userModel.Id);
            if (user == null)
            {
                return "No User"; // Or throw an exception if preferred
            }

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

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                // Log exception
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