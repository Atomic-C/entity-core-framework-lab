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
        //public AuthorBook AuthorBook { get; set; } 
        public Book Book { get; set; }

        //IEnumerable<AuthorBook> AuthorBookList { get; set; }
        IEnumerable<SelectListItem> AuthorList { get; set; }
    }
}
