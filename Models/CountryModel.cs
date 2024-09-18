using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class CountryModel : BaseModel
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Language { get; set; }
                
    }
}