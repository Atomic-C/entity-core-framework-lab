using MagicLib_DataAccess.Data;
using MagicLib_Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicLib.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db; // We need this when we pass data to our view,  DBContext!
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {


            return View(); // pass the list to the view
        }

        // GET for Upsert
        public IActionResult Upsert(int? id) // This populates decides which view we get
        {

            return View();

        }
        // POST for Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPost(Author obj) // This method creates or updates Authors
        {

            return View();

        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}
