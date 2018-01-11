using System.Web.Mvc;

namespace Webshop.Features.ProductRegistration
{
    public class ProductRegistrationController : Controller
    {
        private readonly ProductStoringDAOFactory _productsDAOFactory;

        public ProductRegistrationController(ProductStoringDAOFactory productsDAOFactory)
        {
            _productsDAOFactory = productsDAOFactory;
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
                var workingOnMemory = (bool)(Session?["WorkingOnMemory"] ?? true);
                var productsDAO = _productsDAOFactory.Create(workingOnMemory);
                var result = ProductRegister.Register(productsDAO, newProduct);
                if (result is SuccessfulProductRegistrationResult)
                    return RedirectToAction("Index", "Home");
                if (result is FailedProductRegistrationResult failedResult)
                    ViewBag.Message = failedResult.Reason;
            }

            return View("", newProduct);
        }
    }
}