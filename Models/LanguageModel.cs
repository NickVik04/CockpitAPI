using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class LanguageModel : BaseModel
    {
        [Required]
        public string Language { get; set; }
        [Required]
        public string Symbol { get; set; }
        public string Brand { get; set; }
        public string Store { get; set; }

    }
}