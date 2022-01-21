using MagicLib_DataAccess.Data;
using MagicLib_Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicLib.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db; // We need this when we pass data to our view,  DBContext!
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Book> bookList = _db.Books.ToList();
            return View(bookList); // pass the list to the view
        }

        //// GET for Upsert
        //public IActionResult Upsert(int? id) // Show empty object if id is null, show populated object from db if id isn't null
        //{
        //    Book bookObject = new Book();

        //    if (id == null)
        //    {
        //        //publisherObject.Name = "Rename me";
        //        //publisherObject.Location = "Pedro was here too!";
        //        return View(bookObject);
        //    }

        //    bookObject = _db.Books.FirstOrDefault(u => u.Book_Id == id);

        //    if (bookObject == null) // Safe guard for URL trols at this point
        //    {
        //        return NotFound();
        //    }

        //    else // if (id != null)
        //    {
        //        return View(bookObject);
        //    }
        //}

        ///// <summary> 
        ///// Takes Publisher obj passed in the post method form from Upsert View 
        ///// <para>Verify if data annotation requirments are met</para> 
        ///// <para>If Id is null we create</para> 
        ///// <para>If Id is populated, we're editing</para> 
        ///// </summary> 
        //// POST for Upsert
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult UpsertPost(Book obj)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (obj.Book_Id == 0)
        //        {
        //            _db.Books.Add(obj);
        //        }
        //        else
        //        {
        //            _db.Books.Update(obj);
        //        }

        //        _db.SaveChanges();

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(obj);
        //}

        //public IActionResult Delete(int id)
        //{
        //    Book bookObj = _db.Books.FirstOrDefault(u => u.Book_Id == id);

        //    _db.Books.Remove(bookObj);
        //    _db.SaveChanges();

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
