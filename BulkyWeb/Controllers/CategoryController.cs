using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _uow.Category.GetAll().ToList();
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
                _uow.Category.Add(obj);
                _uow.Save();
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
            Category EditedObj = _uow.Category.Get(u=>u.Id==Id);
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
                _uow.Category.Update(obj);
                _uow.Save();
                TempData["success"] = "Your Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {

            if (id != null)
            {
                Category? obj = _uow.Category.Get(u => u.Id == id);
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
                _uow.Category.Remove(obj);
                _uow.Save();
                TempData["success"] = "Your Category Removed Successfully";

                return RedirectToAction("Index");
            }

            return NotFound();
        

        }
    }
}
