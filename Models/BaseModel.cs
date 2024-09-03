using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApi.Models
{
    public abstract class BaseModel
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }  // Primary GUID auto-generated

        [Required]
        [StringLength(10)]
        public string Code { get; set; }  // Unique code

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Auto-inserted at record insert time

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;  // Auto-updated at record update time

        public string CreatedBy { get; set; }  // User ID/GUID of user who created the record

        public string UpdatedBy { get; set; }  // User ID/GUID of user who last updated the record

        [Required]
        public bool IsDeleted { get; set; } = false;  // Boolean default false or 0 (zero)

        public BaseModel(){
            Id = Guid.NewGuid().ToString();
        }
    }
}