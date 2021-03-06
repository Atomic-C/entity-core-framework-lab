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
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db; // We need this when we pass data to our view,  DBContext!
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Author> authorList = _db.Authors.AsNoTracking().ToList(); // Added AsNoTracking since it's readonly
            return View(authorList); // pass the list to the view
        }

        // GET for Upsert
        public IActionResult Upsert(int? id) // This populates decides which view we get
        {
            Author authorObject = new Author();

            if (id == null)
            {
                return View(authorObject);
            }
            authorObject = _db.Authors.FirstOrDefault(u => u.Author_Id == id);

            if (authorObject == null)
            {
                return NotFound();
            }

            else
            {
                return View(authorObject);
            }
        }

        // POST for Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPost(Author obj) // This method creates or updates Authors
        {
            if (ModelState.IsValid)
            {
                if (obj.Author_Id == 0)
                {
                    _db.Authors.Add(obj);
                }
                else
                {
                    _db.Authors.Update(obj);
                }
                     _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            Author authorOject =_db.Authors.FirstOrDefault(u => u.Author_Id == id);

            _db.Authors.Remove(authorOject);

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
