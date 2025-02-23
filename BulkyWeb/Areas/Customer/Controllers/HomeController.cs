using Bulky.DataAccess.Repository.IRepo;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _uow.Product.GetAll(includeProperties: "Category");
            return View(products);
        }
        public IActionResult Details(int? id)
        {
            Product SpecefiedProduct = _uow.Product.Get(p => p.Id == id,includeProperties: "Category");
            if (SpecefiedProduct == null)
            {
                return NotFound();
            }

            return View(SpecefiedProduct);
        }
        //[HttpPost]
        //public IActionResult Details(Product prod)
        //{

        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
