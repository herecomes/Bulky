using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
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
            if (Category == null)
            {
                ModelState.AddModelError("", "There is no such category!");
                TempData["error"] = "There is no such category!";
                RedirectToPage("Index");
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "Category Edited!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
