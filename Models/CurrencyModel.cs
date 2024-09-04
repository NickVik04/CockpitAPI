using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class CurrencyModel : BaseModel
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public int Decimal { get; set; }
        public string Brand { get; set; }
        public string Store { get; set; }
    }
}