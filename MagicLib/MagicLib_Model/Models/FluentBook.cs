using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models
{
    public class FluentBook
    {
        public int Book_Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }

        public int BookDetail_Id { get; set; }
        public FluentBookDetail FluentBookDetail { get; set; } // "Within a Book there is one BookDetail

        public int Publisher_Id { get; set; }
        public FluentPublisher FluentPublisher { get; set; } // "Within a Book there is one Publisher

        public ICollection<FluentAuthor> FluentAuthor { get; set; }
    }
}
