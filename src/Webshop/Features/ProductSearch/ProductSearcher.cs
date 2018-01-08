﻿using System;
using System.Collections.Generic;
using System.Linq;
using Webshop.Types;

namespace Webshop.Features.ProductSearch
{
    public class ProductSearcher
    {
        private readonly IFindProductsByTitleQuery _productsQuery;

        public ProductSearcher(IFindProductsByTitleQuery productsQuery)
        {
            _productsQuery = productsQuery;
        }

        public IProductSearchResult SearchBy(Text searchText)
        {
            ICollection<FoundProduct> foundProducts;
            try
            {
                foundProducts = _productsQuery.Execute(searchText).ToList();
            }
            catch (Exception)
            {
                return new FailedProductSearchResult("There was a problem, please try again."); // Of course here is the opportunity to log the specific exception
            }

            if (foundProducts.Any())
                return new SuccessfulProductSearchResult(foundProducts);

            return new FailedProductSearchResult("There are no matching products.");
        }
    }
}