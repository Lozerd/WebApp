using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Dynamic;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryContext _categoryContext;

        public HomeController(ILogger<HomeController> logger, CategoryContext categoryContext)
        {
            _logger = logger;
            _categoryContext = categoryContext;
        }

        async public Task<IActionResult> Index()
        {
            setCommonViewBag(HttpContext);
            dynamic model = new ExpandoObject();
            if (_categoryContext != null)
            {
                model.Categories = await _categoryContext.GetCategories();
            }
            return View(viewName: "~/Views/Index.cshtml", model: model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}