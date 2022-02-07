using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_Model.Models.ViewModels
{
    public class AuthorBookViewModel
    {
        public AuthorBookMT AuthorBookMT { get; set; }
        public Book Book { get; set; }

        public IEnumerable<AuthorBookMT> AuthorBookList { get; set; } // To display all available authors for the book
        public IEnumerable<SelectListItem> AuthorList { get; set; } // To display authors in a dropdown list
    }
}
