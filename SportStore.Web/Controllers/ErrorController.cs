using Microsoft.AspNetCore.Mvc;

namespace SportStore.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Error()
        {
            return View();
        }
    }
}