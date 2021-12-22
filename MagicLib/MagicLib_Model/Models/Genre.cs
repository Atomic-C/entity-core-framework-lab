using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models
{
    [Table("Genre_table")]
    public class Genre
    {

        [Key]
        public int GenreId { get; set; }

        [Column("Genre_name_column")]
        public string GenreName { get; set; }
       // public int DisplayOrder { get; set; }
        public int Quantity { get; set; }
    }
}
