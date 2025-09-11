using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureLoginMVC.Data;
using SecureLoginMVC.Models;

namespace SecureLoginMVC.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.Products.AsNoTracking().ToListAsync();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new Product());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Products.Add(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Product \"{model.Name}\" has been successfully created!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            _db.Update(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Product \"{model.Name}\" has been successfully updated!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            TempData["Error"] = "Product deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
