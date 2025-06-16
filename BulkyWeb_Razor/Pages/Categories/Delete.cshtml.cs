using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            //Category? categoryFromDb = _db.Categories.Find(id);
            Category = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (id == null || id == 0)
            {
                Category = null;
            }
        }
        public IActionResult OnPost(int? id)
        {
            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                TempData["error"] = "There is no such category!";
                return RedirectToPage("Index");
            }
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted!";
            return RedirectToPage("Index");
        }
    }
}
