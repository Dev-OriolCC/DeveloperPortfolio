using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class PostDTO
    {
        // Add table columns for database migration
        [Key]
        public int PostID { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        [MaxLength(256)]
        public string PostImage { get; set; }

        [Required]
        [MaxLength(512)]
        public string Excerpt { get; set; }

        [Required]
        [MaxLength(65536)]
        public string Content { get; set; }

        [Required]
        public Boolean IsPublished { get; set; }

        [Required]
        [MaxLength(128)]
        public string Author { get; set; }


        // Foreing key (CategoryID)
        [Required]
        public int CategoryID { get; set; }

    }
}
