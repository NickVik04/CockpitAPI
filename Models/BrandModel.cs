using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class BrandModel
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Logo { get; set; }
        public string Stores { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public string SMSSenderID { get; set; }
        public string EmailSenderID { get; set; }
        [EmailAddress]
        public string SenderEmailAddress { get; set; }
        [MaxLength(21)]
        public string CIN { get; set; }

        public BrandModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}