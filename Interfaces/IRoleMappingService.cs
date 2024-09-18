using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface IRoleMappingService
    {
        Task<IEnumerable<RoleMappingModel>> GetRoleMappings();
        Task<RoleMappingModel> GetRoleMappingByIdAsync(string id);
        Task<string> AddRoleMappingAsync(RoleMappingModel RoleMapping);
       // Task<string> UpdateRoleMappingAsync(string id, RoleMappingModel RoleMapping);
        Task<string> DeleteRoleMappingAsync(string id);
        Task<List<UserRoleMappingDetailModel>>GetUserRoleMapping();
    }
}