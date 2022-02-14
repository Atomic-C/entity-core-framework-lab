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
            List<Book> bookList = _db.Books.AsNoTracking().Include(u => u.Publisher).Include(t=>t.AuthorBookMT).ThenInclude(j=>j.Author).ToList(); // This is eager loading, best otimization! We retrive Publisher

            //List<Book> bookList = _db.Books.ToList(); // This is eager loading, best otimization! We retrive Publisher
            //foreach (var item in bookList) // This is N + 1 execution.
            //{
            //    // Least efficient
            //    //item.Publisher = _db.Publishers.FirstOrDefault(u=>u.Publisher_Id == item.Publisher_Id);

            //    // Explicit loading more efficient yeeeeeeeee ^^
            //    _db.Entry(item).Reference(u => u.Publisher).Load(); // This is how we load publisher so view can be populated.
            //    _db.Entry(item).Collection(u => u.AuthorBookMT).Load(); // This is how we load publisher so view can be populated.
            //    foreach (var authorBook in item.AuthorBookMT)
            //    {
            //        _db.Entry(authorBook).Reference(u => u.Author).Load();
            //    }
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
            //if (ModelState.IsValid) // TODO: Figure out why validation gives an error
            //{

            // AsNoTracking: Solves "The instance of entity type 'Book' cannot be tracked because another instance with the same key value for {'Book_Id'} is already being tracked."


            

            //IQueryable<Book> BookList2 = _db.Books; // Works to get BookDetail_Id
            //List<Book> filteredBookDetails = BookList2.AsNoTracking().Where(b => b.Book_Id == obj.Book.Book_Id).ToList(); // *
            //obj.Book.BookDetail_Id = filteredBookDetails.ElementAt(0).BookDetail_Id;

            //obj.Book = _db.Books.Include(u => u.BookDetail).FirstOrDefault(u=>u.Book_Id == obj.Book.Book_Id); // Eager loading fixes the BookDetail_Id being null, now we no longer lose BookDetail when editing book! 

            if (obj.Book.Book_Id == 0)
                {
                    _db.Books.Add(obj.Book); // When we save Book, creating a Book_Id
                }
                else
                {

                    Book BookFromDb = _db.Books.AsNoTracking().FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id); // Get BookFromDb...
                    obj.Book.BookDetail_Id = BookFromDb.BookDetail_Id; // We assign the BookDetail_Id and solved! No more Null!!

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
                _db.BookDetails.Add(obj.Book.BookDetail); // When we save BookDetail, creating a BookDetail_Id

                _db.SaveChanges(); // Note that BookDetail_Id was empty before this statement was executed

                Book BookFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id); // Load and store book here
                BookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id; // We have to retrive id of the new BookDetail that was saved and update BookFromDb
                _db.SaveChanges(); // And save it on the Book table.
                // Chapter 6 lesson 5
            }
            else
            {
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();

            }
            // When ever we save a record in BookDetail we have to update the corresponding value inside the Book else it shows NULL
            // When a Book is added .Add() , we need to know what is the new BookDetail_Id that was created.

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
        /// <summary>
        /// Receives id, populates  AuthorBookList with eagerloading .Include(), so navigation to properties is available.
        /// Where() 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ManageAuthors(int id)
        {
            AuthorBookViewModel authorBookViewModel = new AuthorBookViewModel()
            {// populate authorbooklist for all of the books to manage author for a specific book
                // we include author by eagerloading it and so on
                AuthorBookList = _db.AuthorBookMT.Include(u => u.Book).Include(u => u.Author).Where(u => u.Book_Id == id).ToList(),

                AuthorBookMT = new AuthorBookMT() // We need this because, 
                {
                    Book_Id = id // we need to set the id for the Book_Id in case it's the first time we create an author or assigning an author for the book.
                },
                Book = _db.Books.FirstOrDefault(u => u.Book_Id == id) // Here we retrive the record for the book
            }; // by here our authorBookViewModel has been populated.

            // List of authors assigned to current book below
            List<int> tempListOfAssignedAuthors = authorBookViewModel.AuthorBookList.Select(u => u.Author_Id).ToList();
            // Projections - The ability to request only certain columns from the database when writing your query
            // https://benjii.me/2018/01/expression-projection-magic-entity-framework-core/#:~:text=A%20projection%20is%20just%20a%20way%20of%20mapping,be%20altered%2Fjoined%2Fremoved%20on%20the%20way%20through%20as%20well.

            // NOT IN Clause
            // Get all the authos whos id is not in tempListOfAssignedAuthors
            var tempList = _db.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id)).ToList();

            // projections were used to populate dropdown
            authorBookViewModel.AuthorList = tempList.Select(k => new SelectListItem // Dropdown only has authors not assigned to book!
            {
                Text = k.FullName,
                Value = k.Author_Id.ToString()
            });

            return View(authorBookViewModel);
        }

        [HttpPost]
            public IActionResult ManageAuthors(AuthorBookViewModel authorBookViewModel)
           {// Book_Id and Author_Id cannot be zero
            if (authorBookViewModel.AuthorBookMT.Book_Id !=0 && authorBookViewModel.AuthorBookMT.Author_Id !=0) 
            {
                _db.AuthorBookMT.Add(authorBookViewModel.AuthorBookMT); // TRY changing id to zero on debug to see what happens
                _db.SaveChanges();

            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = authorBookViewModel.AuthorBookMT.Book_Id });
            //return RedirectToAction(nameof(ManageAuthors);
           }


        public IActionResult RemoveAuthors(int authorId, AuthorBookViewModel authorBookViewModel) // Book_Id and Author_Id are in hidden fields here
        {


            // we have Author_Id and Book_Id inside authorBookViewModel
            AuthorBookMT authorBookMT = _db.AuthorBookMT.FirstOrDefault(u => u.Author_Id == authorId && u.Book_Id == authorBookViewModel.Book.Book_Id); // We extract AuthorBookMT

            int bookIdStorage = authorBookMT.Book_Id; // We store this because we need it on return since it's deleted before then on _db.Remove(authorBookMT);


            _db.Remove(authorBookMT);
            _db.SaveChanges();

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookIdStorage });
        }

        /// <summary> 
        /// Differed execution function examples 
        /// <para>So that I can understand at what stage differed execution is taking place</para> 
        /// <para>So I can force it's execution at place where I want it.</para> 
        /// </summary> 
        public IActionResult PlayGround ()
        {
            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //double totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _db.Books.Count();

            //IEnumerable<Book> BookList = _db.Books; 
            //List<Book> filteredBook1 = BookList.Where(b => b.Price > 1).ToList(); // Filters in memory, loads server

            //IQueryable<Book> BookList2 = _db.Books;
            //List<Book> filteredBook2 = BookList2.Where(b => b.Price > 1).ToList(); // Filters in database, less server load. <= Use it

            // Here we change the entity state
            var category = _db.Categories.FirstOrDefault();
            _db.Entry(category).State = EntityState.Modified; // This is how to manually change entity state, this is manually updating 

            _db.SaveChanges(); 

            // Updating related data
            Book bookThePowerOfNow = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b=>b.Book_Id == 9);
            bookThePowerOfNow.BookDetail.NumberOfChapters = 1000;
            _db.Books.Update(bookThePowerOfNow);
            _db.SaveChanges();

            Book bookAtomicHabits = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b=>b.Book_Id == 11);
            bookAtomicHabits.BookDetail.NumberOfPages = 5000;
            _db.Books.Attach(bookAtomicHabits); // updates only BookDetails! not all Book like Update()
            _db.SaveChanges();
            // If something on unchanged state is modified, attach will change the state to modified as well.


            // Todo: Figure out more differences between Attach() and Update()

            return RedirectToAction(nameof(Index)); // We can do this redirect also when we don't a view associated with method
        }
    }
}
