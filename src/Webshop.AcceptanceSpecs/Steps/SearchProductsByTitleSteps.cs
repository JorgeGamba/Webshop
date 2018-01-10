using System.Collections.Generic;
using System.Web.Mvc;
using FluentAssertions;
using TechTalk.SpecFlow;
using Webshop.Features.ProductSearch;

namespace Webshop.AcceptanceSpecs.Steps
{
    [Binding]
    public class SearchProductsByTitleSteps
    {
        private InMemoryStorage _inMemoryStorage;
        private readonly ProductSearchController _controller;
        private ViewResult _result;
        private string _searchText;

        public SearchProductsByTitleSteps()
        {
            _inMemoryStorage = new InMemoryStorage();
            var productsQuery = new FindProductsByTitleMemoryQuery(_inMemoryStorage);
            var searcher = new ProductSearcher(productsQuery);
            _controller = new ProductSearchController(searcher);
        }

        [Given]
        public void Given_I_have_provided_a_text_to_search()
        {
            _searchText = "cd";
        }

        [Given]
        public void Given_I_have_provided_an_invalid_text_to_search()
        {
            _searchText = "";
        }

        [Given]
        public void Given_there_are_no_products_that_match_the_searched_text()
        {
            _inMemoryStorage.Insert(new StoredProduct(1, 111, "Product abc", 1, "first product"));
            _inMemoryStorage.Insert(new StoredProduct(4, 114, "Product def", 4, "fourth product"));
        }

        [Given]
        public void Given_there_are_multiple_products_that_match_the_searched_text()
        {
            _inMemoryStorage.Insert(new StoredProduct(1, 111, "Product abc", 1, "first product"));
            _inMemoryStorage.Insert(new StoredProduct(2, 112, "Product bcd", 2, "first product"));
            _inMemoryStorage.Insert(new StoredProduct(3, 113, "Product cde", 3, "first product"));
            _inMemoryStorage.Insert(new StoredProduct(4, 114, "Product def", 4, "first product"));
        }
        
        [When]
        public void When_I_request_the_search()
        {
            _result = _controller.Index(_searchText);
        }

        [Then]
        public void Then_I_should_be_informed_that_no_matches_were_found()
        {
            _result.Model.Should().BeOfType<FailedProductSearchResult>();
        }

        [Then]
        public void Then_I_should_be_able_to_try_again_with_other_criteria()
        {
            _result.ViewName.Should().Be(""); // The "" means the default view for the action method 
        }
        
        [Then]
        public void Then_I_should_see_all_the_matching_products()
        {
            FoundProducts.Should().HaveCount(2);
            FoundProducts.Should().OnlyContain(x => x.Number == 112 || x.Number == 113);
        }
        
        [Then]
        public void Then_I_should_be_informed_that_my_search_request_is_invalid()
        {
            _result.ViewData.ModelState.IsValid.Should().BeFalse();
        }


        private ICollection<FoundProduct> FoundProducts => ((SuccessfulProductSearchResult)_result.Model).FoundProducts;
    }
}
