using System.Web.Mvc;

namespace Webshop.Features.ProductRegistration
{
    public class ProductRegistrationController : Controller
    {
        private readonly ProductRegister _register;

        public ProductRegistrationController(ProductRegister register)
        {
            _register = register;
        }

        // GET
        public ViewResult Index()
        {
            return View("", new NewProductInputModel());
        }

        [HttpPost]
        public ActionResult Index(NewProductInputModel newProduct)
        {
            if (ModelState.IsValid)
            {
                var result = _register.Register(newProduct);
                if (result is SuccessfulProductRegistrationResult)
                    return RedirectToAction("Index", "Home");
                if (result is FailedProductRegistrationResult failedResult)
                    ViewBag.Message = failedResult.Reason;
            }

            return View("", newProduct);
        }
    }
}