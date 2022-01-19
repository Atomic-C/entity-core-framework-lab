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
            List<Publisher> publisherList = _db.Publishers.ToList();
            return View(publisherList); // pass the list to the view
        }

        // GET for Upsert
        public IActionResult Upsert(int? id) // This populates decides which view we get
        {
            Publisher publisherObject = new Publisher();

            if (id == null)
            {
                return View(publisherObject);
            }

                return View();
        }       
        // POST for Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPost(Publisher obj) // This method creates or updates Publishers
        {
                return View();
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
