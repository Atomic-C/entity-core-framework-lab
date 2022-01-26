using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models
{
    public class BookDetail // BookDetail and Book will have a 1:1 relationship
    {
        [Key]
        public int BookDetail_Id { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public int Weight { get; set; }

        public Book Book { get; set; } // This enforces a 1:1 relationship with Book
    }
}
