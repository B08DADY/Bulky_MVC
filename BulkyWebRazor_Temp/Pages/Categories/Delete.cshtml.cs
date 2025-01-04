using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public Category cat { get; set; }
        public BulkyRazorDbContext _db;
        public DeleteModel(BulkyRazorDbContext db)
        {
            _db = db;
            
        }

        public void OnGet(int Id)
        {
            cat = _db.Categories.Find(Id);

        }
        public IActionResult OnPost()
        {
            if (cat != null)
            {
                _db.Categories.Remove(cat);
                _db.SaveChanges();
                TempData["success"] = "Category removed successfully";
                return RedirectToPage("Index");
            }
            return NotFound();


        }
    }
}
