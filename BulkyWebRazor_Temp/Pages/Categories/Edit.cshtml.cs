using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Principal;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly BulkyRazorDbContext _db;
        public Category cat { get; set; }
        public EditModel(BulkyRazorDbContext db)
        {
            _db = db;
        }

        
        public void OnGet(int Id)
        {
            if (Id != null && Id != 0)
            {
                cat = _db.Categories.Find(Id);
            }
            
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid )
            {
                _db.Categories.Update(cat);
                _db.SaveChanges();
                TempData["success"] = "Category Updated successfully";

                return RedirectToPage("index");
            }
            return NotFound();

        }
    }
}
