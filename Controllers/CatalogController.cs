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
    public class CatalogController : BaseController
    {
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(CatalogContext catalogContext, ILogger<CatalogController> logger) : base(catalogContext)
        {
            _logger = logger;
        }

        [Route("catalog", Name = "catalog")]
        //public async Task<IActionResult> Index(int? category_id)
        public async Task<IActionResult> Catalog()
        {
            List<Product> products = await CatalogContext.Products.ToListAsync();
            string template_name = "~/Views/Category/Catalog.cshtml";
            SetCommonContext(HttpContext);
            Model.Categories = await CatalogContext.Categories.ToListAsync();
            Model.Products = products;
            return View(template_name, Model);
        }
        
        [Route("catalog/{category_id}", Name = "catalog_detail")]
        public async Task<IActionResult> CatalogDetail([FromQuery]int? category_id)
        {
            string template_name = "~/Views/Category/Catalog.cshtml";
            
            if (category_id == null)
            {
                return NotFound();
            }

            Category category = await CatalogContext.Categories.FirstOrDefaultAsync(c => c.Id == category_id);
            
            if (category == null)
            {
                return NotFound();
            }
            SetCommonContext(HttpContext);
            Model.Category = category;
            Model.Products = CatalogContext.GetCategoryRelatedProducts(category_id ?? 0);
            return View(template_name, Model);
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
