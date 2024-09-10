using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class LanguageModel : BaseModel
    {
        public string Language { get; set; }
        public string Symbol { get; set; }
        public string Brand { get; set; }
        public string Store { get; set; }

    }
}