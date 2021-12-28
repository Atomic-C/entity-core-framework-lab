using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models
{
    public class FluentAuthor
    {
        public int Author_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }

        public string FullName 
        { 
        get 
            { 
                return $"{FirstName} {LastName}"; 
            }
        }

        /* EF Relations */
        // https://henriquesd.medium.com/entity-framework-core-5-0-many-to-many-relationships-52c6c8b07b6e
        public ICollection<Book> Books { get; set; }
    }
}
