using MagicLib_DataAccess.Data;
using MagicLib_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Publisher> publisherList = _db.Publishers.AsNoTracking().ToList(); // Added AsNoTracking since it's readonly
            return View(publisherList); // pass the list to the view
        }

        // GET for Upsert
        public IActionResult Upsert(int? id) // Show empty object if id is null, show populated object from db if id isn't null
        {
            Publisher publisherObject = new Publisher();

            if (id == null)
            {
                //publisherObject.Name = "Rename me";
                //publisherObject.Location = "Pedro was here too!";
                return View(publisherObject);
            }

            publisherObject = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == id);

            if (publisherObject == null) // Safe guard for URL trols at this point
            {
                return NotFound();
            }

            else // if (id != null)
            {
                return View(publisherObject);
            }
        }

        /// <summary> 
        /// Takes Publisher obj passed in the post method form from Upsert View 
        /// <para>Verify if data annotation requirments are met</para> 
        /// <para>If Id is null we create</para> 
        /// <para>If Id is populated, we're editing</para> 
        /// </summary> 
        // POST for Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPost(Publisher obj)
        {
            if (ModelState.IsValid)
            {

                if (obj.Publisher_Id == 0)
                {
                    _db.Publishers.Add(obj);
                }
                else
                {
                    _db.Publishers.Update(obj);
                }

                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            Publisher publisherObj = _db.Publishers.FirstOrDefault(u=>u.Publisher_Id == id);

            _db.Publishers.Remove(publisherObj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
