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
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db; // We can access _db to access our models and use EF to retrive, save, perform CRUD operations
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.AsNoTracking().ToList(); // Added AsNoTracking since it's readonly
            return View(categoryList);
        }

        // GET for Upsert
        public IActionResult Upsert(int? id)
        {

            Category obj = new Category();
            
            if (id == null) // Create
            {
                return View(obj); // Return object empty, since the user will fill the details then submit the button
            }
                // If there'd an id, well, find it in db and retrive it! We must display to edit!
                obj = _db.Categories.FirstOrDefault(u => u.Category_Id == id);

            if (obj == null) // If object is null(doesn't exist, duh)
            {
                return NotFound();
            }
            // Else, id is not null and we can edit!
            else
            {
                return View(obj); // Display it!
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj) // We retrive the category object that was posted
        {
            if (ModelState.IsValid) // Verify if data annotation requirments are met
            {
                if (obj.Category_Id == 0)
                {
                    // We're creating
                    //_db.Add(obj); // Alternative to line below:
                    _db.Categories.Add(obj);
                }
                else
                {
                    // We're updating then
                    _db.Categories.Update(obj);
                }
                    _db.SaveChanges();
                return RedirectToAction(nameof(Index)); // We return to our displayed list presuming
            }
            return View(obj); // Presuming modelstate is invalid we return back to the view, obj will display errors.
        }

        public IActionResult Delete(int id)
        {
            Category obj = _db.Categories.FirstOrDefault(u => u.Category_Id == id);
            _db.Categories.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteWithoutPassingId()
        {
            List<Category> catList = _db.Categories.OrderByDescending(u => u.Category_Id).Take(1).ToList();
            _db.Categories.RemoveRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {
            List<Category> catList = new List<Category>();
            for (int i = 1; i <= 2; i++)
            {
                catList.Add(new Category { 
                    Name = Guid.NewGuid().ToString()
                });
                //_db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() }); // Using this without the list is the same.
            }
            _db.Categories.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }       
        public IActionResult CreateMultiple5()
        {
            for (int i = 1; i <= 5; i++)
            {
                _db.Categories.Add(new Category {
                    Name = Guid.NewGuid().ToString()
                });
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple2() 
        {
            //var obj = _db.Categories.OrderByDescending(u=>u.Category_Id).Take(2);
            List<Category> catList = _db.Categories.OrderByDescending(u => u.Category_Id).Take(2).ToList();
          
            _db.Categories.RemoveRange(catList);

            
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            var obj = _db.Categories.OrderByDescending(u => u.Category_Id).Take(5);


            _db.Categories.RemoveRange(obj);


            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
