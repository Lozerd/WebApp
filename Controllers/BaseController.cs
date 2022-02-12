using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        // Sets the global context
        protected void setCommonViewBag(HttpContext request)
        {
            ViewBag.RequestPath = request.Request.Path;
        }
    }
}
