using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;

namespace IdentityApi.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationModel>> GetApplication();
        Task<ApplicationModel> GetApplicationByIdAsync(string id);
        Task<string> AddApplicationAsync(ApplicationModel Application);
        Task<string> UpdateApplicationAsync(string id, ApplicationModel Application);
        Task<string> DeleteApplicationAsync(string id);
        
    }
}