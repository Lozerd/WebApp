using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoryController1 : BaseController
    {

        public IActionResult CreateCategory()
        {
            return View(viewName: "~/Views/");
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            return RedirectToAction("Index");
        }


        public IActionResult ShopGrid()
        {
            setCommonViewBag(HttpContext);
            return View(viewName: "~/Views/Category/shop-grid.cshtml");
        }
    }
}
