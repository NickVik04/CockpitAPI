using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class UserRoleMappingDetailModel : BaseModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ContactNumber { get; set; }
        public string CockpitRole { get; set; }
        public string Applications { get; set; }
        public string Status { get; set; }
    }
}