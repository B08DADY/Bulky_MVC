using Bulky.DataAccess.Migrations;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepo;
using Bulky.Models;
using Bulky.Models.ViewModles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BulkyWeb.Areas.Admin.Controllers
{

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment)
        {
            _uow = uow;
            _webHostEnvironment = webHostEnvironment;


        }
        public IActionResult Index()
        {
            List<Product> prods = _uow.Product.GetAll("Category").ToList();

            return View(prods);
        }

        public IActionResult UpSert(int? id)
        {
            ProductVM product = new ProductVM()
            {

                CategoryList = _uow.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            //update
            if (id != 0 && id != null)
            {
                product.Product = _uow.Product.Get(u => u.Id == id);

            }
            //create
            else
            {
                product.Product = new Product();

            }


            return View(product);

        }

        [HttpPost]
        public IActionResult UpSert(ProductVM productt, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                //adding the image to wwwroot\Images\Product
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(wwwRootPath, @"Images\Product");
                    if (!string.IsNullOrEmpty(productt.Product.ImgUrl))
                    {
                        var DeletedImgPath = Path.Combine(wwwRootPath, productt.Product.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(DeletedImgPath))
                        {
                            System.IO.File.Delete(DeletedImgPath);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productt.Product.ImgUrl = @"\Images\Product\" + fileName;
                }
                //if (productt.Product.ImgUrl == null)
                //    productt.Product.ImgUrl = "";



                if (productt.Product.Id == 0)
                {
                    _uow.Product.Add(productt.Product);
                }
                else
                {
                    _uow.Product.Update(productt.Product);
                }

                _uow.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productt.CategoryList = _uow.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productt);
            }

        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _uow.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete("admin/product/delete/{id}")]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _uow.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           productToBeDeleted.ImgUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _uow.Product.Remove(productToBeDeleted);
            _uow.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
