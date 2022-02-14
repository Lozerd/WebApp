using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected dynamic Model = new ExpandoObject();
        protected readonly CatalogContext CatalogContext;
        public BaseController(CatalogContext catalogContext)
        {
            CatalogContext = catalogContext;
        }

        public void SetCommonContext(HttpContext request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            SetCommonViewBag(request);
            SetCommonModels();
        }

        private void SetCommonViewBag(HttpContext request)
        {
            ViewBag.RequestPath = request.Request.Path;
        }

        private void SetCommonModels()
        {
            Model.Categories = CatalogContext.Categories.ToList();
        }
    }
}
