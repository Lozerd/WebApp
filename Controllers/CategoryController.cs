#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CatalogContext catalogContext, ILogger<CategoryController> logger) : base(catalogContext)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("catalog/{category_id?}/", Name = "catalog")]
        //public async Task<IActionResult> Index(int? category_id)
        public IActionResult Index(int? category_id)
        {
            string template_name = "~/Views/Category/Catalog.cshtml";
            SetCommonContext(HttpContext);
            Model.Category = CatalogContext.Categories.FirstOrDefault(c => c.Id == category_id);
            Model.Products = CatalogContext.Products.Where(p => p.CategoryId == category_id).ToList();//GetCategoryRelatedProducts(Model.Category.Id);
            return View(template_name, Model);
        }
        
        [HttpGet]
        [Route("category/detail/{id?}/", Name = "category_detail")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await CatalogContext.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            _logger.LogInformation("create " + HttpContext.Request.Method.ToString());
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Category category)
        {
            _logger.LogInformation("create POST");
            if (ModelState.IsValid)
            {
                CatalogContext.Add(category);
                await CatalogContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await CatalogContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CatalogContext.Update(category);
                    await CatalogContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await CatalogContext.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await CatalogContext.Categories.FindAsync(id);
            CatalogContext.Categories.Remove(category);
            await CatalogContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return CatalogContext.Categories.Any(e => e.Id == id);
        }
    }
}
