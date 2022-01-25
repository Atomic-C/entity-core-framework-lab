using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MagicLib_Model.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; } // This SelectListItem is for the dropdown! It's stored here.
        // Notice we do not have any Publisher inside this list unlike Book in Book in the line above!
    }
}
