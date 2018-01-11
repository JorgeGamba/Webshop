using System.Web.Mvc;
using Webshop.Types;

namespace Webshop.Features.ProductSearch
{
    public class ProductSearchController : Controller
    {
        private readonly FindProductsByTitleQueryFactory _productsQueryFactory;

        public ProductSearchController(FindProductsByTitleQueryFactory productsQueryFactory)
        {
            _productsQueryFactory = productsQueryFactory;
        }

        // GET
        public ViewResult Index(string titleContains)
        {
            if (Text.TryCreate(titleContains, out var searchText))
            {
                var workingOnMemory = (bool)(Session?["WorkingOnMemory"] ?? true);
                var productsQuery = _productsQueryFactory.Create(workingOnMemory);
                var result = ProductSearcher.Search(productsQuery, searchText);
                return View(result);
            }

            ModelState.AddModelError("", "The text to search is not valid");
            return View();
        }
    }
}