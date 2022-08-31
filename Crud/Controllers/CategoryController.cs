
using Crud.DataAccess;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Main page where all content is displayed
        public IActionResult Index()
        {
            var objCategory = _db.Categories.ToList();
            return View(objCategory);
        }

        // Page that shows a form to create
        public IActionResult Create()
        {
            return View();
        }
       [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category Created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // To edit a row
        public IActionResult Edit(int id)
        {
            var row = _db.Categories.Find(id);
            return View(row);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category data)
        {
            var row = _db.Categories.Find(data.Id);
            row.Name = data.Name;
            row.DisplayOrder = data.DisplayOrder;
            _db.Categories.Update(row);
            _db.SaveChanges();
            TempData["success"] = "Category Edited successfully";

            return RedirectToAction("Index");
        }

        // To Delete a row

        public IActionResult Delete(int id)
        {
            var row = _db.Categories.Find(id);

            return View(row);
        }
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var row = _db.Categories.Find(id);

            if (row == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(row);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
