using System.Collections.Generic;

namespace Webshop.Features.ProductSearch
{
    public interface IProductSearchResult
    {
    }

    public class SuccessfulProductSearchResult : IProductSearchResult
    {
        public SuccessfulProductSearchResult(ICollection<FoundProduct> foundProducts)
        {
            FoundProducts = foundProducts;
        }

        public ICollection<FoundProduct> FoundProducts { get; }
    }

    public class FailedProductSearchResult : IProductSearchResult
    {
        public FailedProductSearchResult(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; }
    }
}