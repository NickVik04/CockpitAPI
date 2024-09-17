using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class RoleMappingModel : BaseModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string ApplicationID { get; set; }
        [Required]
        public string RoleID { get; set; }
    }
}