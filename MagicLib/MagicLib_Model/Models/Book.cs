using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models
{
    public class Book
    {
        [Key]
        public int Book_Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }

        [ForeignKey("Category")] // What column do we reference?
        public int Category_Id { get; set; } // // This is called a navigation property. We can rename FK here.
        public Category Category { get; set; } // This is the Foreign Key relation between Book and Categories
    }
}
