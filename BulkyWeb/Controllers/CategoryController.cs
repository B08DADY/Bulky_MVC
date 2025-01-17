using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApllicationDbContext _db;
        public CategoryController(ApllicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {


            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Your Category Added Successfully";

                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category EditedObj = _db.Categories.Find(Id);
            if (EditedObj == null)
            {
                return NotFound();
            }

            return View(EditedObj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {


            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Your Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {

            if (id != null)
            {
                Category? obj = _db.Categories.Find(id);
                if (id != null)
                {
                    return View(obj);
                }

            }
            return NotFound();

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Category obj)
        {
        //    Category? obj= _db.Categories.Find(id);
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Your Category Removed Successfully";

                return RedirectToAction("Index");
            }

            return NotFound();
        

        }
    }
}
