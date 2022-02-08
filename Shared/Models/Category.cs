using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Category
    {
        // Add table columns for database migration
        [Key]
        public int CategoryID { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string BlogImage { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
    }
}
