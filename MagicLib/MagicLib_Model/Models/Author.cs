using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models
{
    public class Author
    {
        [Key]
        public int Author_Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }

        [NotMapped]
        public string FullName 
        { 
        get 
            { 
                return $"{FirstName} {LastName}"; 
            }
        }

        /* EF Relations */
        // https://henriquesd.medium.com/entity-framework-core-5-0-many-to-many-relationships-52c6c8b07b6e
        public ICollection<AuthorBookMT> AuthorBookMT { get; set; } // Made it explicitely becaus we need for VM
    }
}
