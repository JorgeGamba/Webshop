using System.Web.Mvc;

namespace Webshop.Features.Home
{
    public class HomeController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}