using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        // Sets the global context
        protected async void setCommonViewBag(HttpContext request)
        {
            ViewBag.RequestPath = request.Request.Path;
        }
    }
}
