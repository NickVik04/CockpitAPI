using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetRoles();
        Task<RoleModel> GetRoleByIdAsync(string id);
        Task<string> AddRoleAsync(RoleModel Role);
        Task<string> UpdateRoleAsync(string id, RoleModel Role);
        Task<string> DeleteRoleAsync(string id);
    }
}