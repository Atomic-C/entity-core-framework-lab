using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models
{
    public class FluentBookDetail // BookDetail and Book will have a 1:1 relationship
    {
        public int BookDetail_Id { get; set; }
        public int NumberOfCapters { get; set; }
        public int NumberOfPages { get; set; }
        public int Weight { get; set; }

    }
}
