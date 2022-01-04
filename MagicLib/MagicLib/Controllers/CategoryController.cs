using MagicLib_DataAccess.Data;
using MagicLib_Model.Models;
using Microsoft.AspNetCore.Mvc;
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
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Upsert(int? id)
        {

            Category obj = new Category();
            
            if (id == null) // Create
            {
                return View(obj);
            }
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
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
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
    }
}
