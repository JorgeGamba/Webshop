using System.Web.Mvc;

namespace Webshop.Features.SettingsStorage
{
    public class SettingsStorageController : Controller
    {
        // GET
        public ViewResult Index()
        {
            var workingOnMemory = (bool)(Session?["WorkingOnMemory"] ?? true);
            return View(workingOnMemory);
        }

        [HttpPost]
        public ActionResult Change()
        {
            var workingOnMemory = (bool)(Session?["WorkingOnMemory"] ?? true);
            Session["WorkingOnMemory"] = !workingOnMemory;
            return RedirectToAction("Index", "Home");
        }
    }
}