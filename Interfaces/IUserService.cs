using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Interfaces
{
    public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel> GetUserByIdAsync(string id);
    Task<string> AddUserAsync(UserModel userModel);
    Task<string> UpdateUserAsync(string id, UserModel userModel);
    Task<string> DeleteUserAsync(string id);
}
}