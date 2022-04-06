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

        //[ForeignKey("Category")] // What column do we reference?
        //public int Category_Id { get; set; } // // This is called a navigation property. We can rename FK here.
        //public Category Category { get; set; } // This is the Foreign Key relation between Book and Categories


        // We set a Foreign Key relation between Book and BookDetail following the logic above
        [ForeignKey("BookDetailNav")]
        public int? BookDetail_Id { get; set; }
        public BookDetail BookDetail { get; set; } // "Within a Book there is one BookDetail

        //Above Note: If we didn't add the not mapped and it's not in DbSet, EFCore 5 sees we need the above as a property and adds the property\table in the migration.

        [ForeignKey("Publisher")] // FK relation between Book and Publisher
        public int Publisher_Id { get; set; }
        public Publisher Publisher { get; set; } // "Within a Book there is one Publisher

        /* EF Relations */
        // https://henriquesd.medium.com/entity-framework-core-5-0-many-to-many-relationships-52c6c8b07b6e
        public ICollection<AuthorBookMT> AuthorBookMT { get; set; } // Made it explicitely becaus we need for VM
    }
}
