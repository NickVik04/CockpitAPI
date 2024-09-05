using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class StoreModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public int AlternateCode { get; set; }
        public string Type { get; set; }
        [Url]
        public string Url { get; set; }
        [MaxLength(15)]
        public string GSTIN { get; set; }
        [EmailAddress]
        public string EmailID { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zone { get; set; }
        public string Region { get; set; }
        public int Pincode { get; set; }
        [Required]
        public int Timing { get; set; }
        [MaxLength(21)]
        public string CIN { get; set; }
        public string PAN { get; set; }
    }
}