using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class CountryModel : BaseModel
    {
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
                
    }
}