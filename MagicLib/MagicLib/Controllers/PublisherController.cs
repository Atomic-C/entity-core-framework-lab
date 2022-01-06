using MagicLib_DataAccess.Data;
using MagicLib_Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicLib.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db; // We need this when we pass data to our view,  DBContext!
        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            List<Publisher> publisherList = _db.Publishers.ToList(); // How to retrive all Publishers from database

            return View(publisherList); // pass the list to the view
        }

        public IActionResult Upsert(int? id) // This method creates or updates Publishers
        {
            // int? id means id can be null or not, depending on creation or edit\update
            
            Publisher publisher = new Publisher();

            if (id == null) // Means we'll create
            {
                return View(publisher); // Since we'll be creating there's no need to populate the view, we return the empty object.
            }
            // If id isn't null then we want to edit! We need to display from db what we want to edit...
            else
            {
                /*Publisher*/ publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == id); // We retrive Publisher with same id we passed in the filter lambda expression

                //we use FirstOrDefault and this assigns obj another value if filter doesn't retrieve a record.

                if (publisher == null) // If object doesn't exist...like we insert it in url
                {
                    return NotFound();
                }

                return View(publisher);
            }
        }
    }
}
