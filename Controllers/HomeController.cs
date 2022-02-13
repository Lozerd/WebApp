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
        

        public HomeController(ILogger<HomeController> logger, CatalogContext categoryContext) : base(categoryContext)
        {
            _logger = logger;
        }

        async public Task<IActionResult> Index()
        {
            string template_name = "~/Views/Home/Index.cshtml";
            SetCommonContext(HttpContext);
            return View(template_name, Model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}