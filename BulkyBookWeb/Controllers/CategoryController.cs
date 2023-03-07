using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;
            return View(categories);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category newCategory)
        {
            if(newCategory.Name == newCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                TempData["ResultMessage"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(newCategory);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category updatedCategory)
        {
            if (updatedCategory.Name == updatedCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Update(updatedCategory);
                _context.SaveChanges();
                TempData["ResultMessage"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(updatedCategory);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category deletedCategory)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == deletedCategory.Id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["ResultMessage"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            }

        }

    }
}
