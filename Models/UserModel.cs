using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Models
{
    public class UserModel : BaseModel
    {
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string EmailId { get; set; }

        [Phone]
        public string PhoneNo { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        public string Pincode { get; set; }

        [Required]
        public string Brand { get; set; }

        public string Store { get; set; }

        public string Status { get; set; }

    }
}