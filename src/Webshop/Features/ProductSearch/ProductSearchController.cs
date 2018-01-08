using System.Web.Mvc;
using Webshop.Types;

namespace Webshop.Features.ProductSearch
{
    public class ProductSearchController : Controller
    {
        private readonly ProductSearcher _searcher;

        public ProductSearchController(ProductSearcher searcher)
        {
            _searcher = searcher;
        }

        // GET
        public ViewResult Index(string titleContains)
        {
            if (Text.TryCreate(titleContains, out var searchText))
            {
                var result = _searcher.SearchBy(searchText);
                return View(result);
            }

            ModelState.AddModelError("", "The text to search is not valid");
            return View();
        }
    }
}