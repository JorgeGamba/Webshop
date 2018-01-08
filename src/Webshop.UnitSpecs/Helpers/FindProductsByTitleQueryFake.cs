using System;
using System.Collections.Generic;
using Webshop.Features.ProductSearch;
using Webshop.Types;

namespace Webshop.UnitSpecs.Helpers
{
    public class FindProductsByTitleQueryFake : IFindProductsByTitleQuery
    {
        private readonly Func<FoundProduct[]> _getFoundProducts;

        public FindProductsByTitleQueryFake(Func<FoundProduct[]> getFoundProducts)
        {
            _getFoundProducts = getFoundProducts;
        }

        public IEnumerable<FoundProduct> Execute(Text searchText) =>
            _getFoundProducts();
    }
}