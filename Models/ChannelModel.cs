using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class ChannelModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(7)]
        public string ChannelCode { get; set; }
    }
}