using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class ApplicationModel : BaseModel
    {
        [Required]
        public string ApplicationName { get; set; }
    }
}