using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public Category TempCat { get; set; }
        private readonly BulkyRazorDbContext _db;
        public CreateModel(BulkyRazorDbContext db)
        {
            _db = db;
        }
        public void OnGet() { 
           
        }
        public IActionResult OnPost()
        {
            if (TempCat != null)
            {
                _db.Categories.Add(TempCat);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";

                return RedirectToPage("Index");
            }
            return NotFound();

        }
    }
}
