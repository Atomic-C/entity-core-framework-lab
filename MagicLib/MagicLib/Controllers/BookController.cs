using MagicLib_DataAccess.Data;
using MagicLib_Model.Models;
using MagicLib_Model.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            List<Book> bookList = _db.Books.Include(u => u.Publisher).ToList(); // This is eager loading, best otimization! We retrive Publisher
            //foreach (var item in bookList) // This is N + 1 execution.
            //{
            //    // Least efficient
            //    //item.Publisher = _db.Publishers.FirstOrDefault(u=>u.Publisher_Id == item.Publisher_Id);

            //    // Explicit loading more efficient yeeeeeeeee ^^
            //    _db.Entry(item).Reference(u => u.Publisher).Load(); // This is how we load publisher so view can be populated.
            //}
            return View(bookList); // pass the list to the view
        }
        // Chapter 6 4, includes explicit loading.
        // Chapter 6 7, for eager loading.

        // GET for Upsert
        public IActionResult Upsert(int? id) // Show empty object if id is null, show populated object from db if id isn't null
        {
            BookViewModel bookObject = new BookViewModel();
            bookObject.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString(),
            }); // This is a projection :o

            if (id == null)
            {
                //publisherObject.Name = "Rename me";
                //publisherObject.Location = "Pedro was here too!";
                return View(bookObject);
            }

            bookObject.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);

            if (bookObject == null) // Safe guard for URL trols at this point
            {
                return NotFound();
            }

            else // if (id != null)
            {
                return View(bookObject);
            }
        }

        /// <summary> 
        /// Takes BookViewModel obj passed in the post method form from Upsert View 
        /// <para>Verify if data annotation requirments are met</para> 
        /// <para>If Id is null we create</para> 
        /// <para>If Id is populated, we're editing</para> 
        /// </summary> 
        // POST for Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPost(BookViewModel obj)
        {
            //if (ModelState.IsValid)
            //{
                if (obj.Book.Book_Id == 0)
                {
                    _db.Books.Add(obj.Book);
                }
                else
                {
                    _db.Books.Update(obj.Book);

                }

                _db.SaveChanges();

                return RedirectToAction(nameof(Index));

            //}
            //return View(obj);
        }

        // GET for Details
        public IActionResult Details(int? id) // Show empty object if id is null, show populated object from db if id isn't null
        {
            BookViewModel bookObject = new BookViewModel();


            if (id == null)
            {
                //publisherObject.Name = "Rename me";
                //publisherObject.Location = "Pedro was here too!";
                return View(bookObject);
            }
            bookObject.Book = _db.Books.Include(u => u.BookDetail).FirstOrDefault(u=>u.Book_Id == id); // We retrive book and update bookdetail!

            // Old way below
            //bookObject.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id); // We retrive book here
            //bookObject.Book.BookDetail = _db.BookDetails.FirstOrDefault(u => u.BookDetail_Id == bookObject.Book.BookDetail_Id); // We must manually update bookdetail here


            if (bookObject == null) // Safe guard for URL trols at this point
            {
                return NotFound();
            }

            else // if (id != null)
            {
                return View(bookObject);
            }
        }

        /// <summary> 
        /// Takes BookViewModel obj passed in the post method form from Upsert View 
        /// <para>Verify if data annotation requirments are met</para> 
        /// <para>If Id is null we create</para> 
        /// <para>If Id is populated, we're editing</para> 
        /// </summary> 
        // POST for DetailsPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPost(BookViewModel obj)
        {
            //if (ModelState.IsValid)
            //{
            if (obj.Book.BookDetail.BookDetail_Id == 0)
            {
                _db.BookDetails.Add(obj.Book.BookDetail); // When we save BookDetail
                _db.SaveChanges();

                Book BookFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id); // Todo: play with this
                BookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id; // We have to retrive id of the new BookDetail that was saved.
                _db.SaveChanges(); // And save it on the Book table.
                // Chapter 6 lesson 5
            }
            else
            {
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();

            }


            return RedirectToAction(nameof(Index));

            //}
            //return View(obj);
        }

        public IActionResult Delete(int id)
        {
            Book bookObj = _db.Books.FirstOrDefault(u => u.Book_Id == id);

            _db.Books.Remove(bookObj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
