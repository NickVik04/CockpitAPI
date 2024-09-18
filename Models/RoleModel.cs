using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class RoleModel : BaseModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}