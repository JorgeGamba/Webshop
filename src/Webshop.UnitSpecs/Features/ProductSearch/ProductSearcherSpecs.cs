using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using Webshop.Features.ProductSearch;
using Webshop.Types;
using Webshop.UnitSpecs.Helpers;

namespace Webshop.UnitSpecs.Features.ProductSearch
{
    public class ProductSearcherSpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = ProductSearcher.Search(_productsQuery, _someSearchText);

        public class When_matching_products_are_found : ProductSearcherSpecs
        {
            public override void Given() =>
                _productsQuery = ObjectMother.CreateProductQueryThatReturns(_someProduct, _otherProduct);

            [Test]
            public void Should_return_a_result() =>
                _result.Should().NotBeNull();

            [Test]
            public void Should_return_a_result_of_type_SuccessfulProductSearchResult() =>
                _result.Should().BeOfType<SuccessfulProductSearchResult>();

            [Test]
            public void Should_return_a_result_with_all_the_matching_products() =>
                ((SuccessfulProductSearchResult) _result).FoundProducts.Should().Contain(new[] { _someProduct, _otherProduct });
        }

        public class When_matching_products_are_not_found : ProductSearcherSpecs
        {
            public override void Given() =>
                _productsQuery = ObjectMother.CreateProductQueryThatReturns();

            [Test]
            public void Should_return_a_result() =>
                _result.Should().NotBeNull();

            [Test]
            public void Should_return_a_result_of_type_FailedProductSearchResult() =>
                _result.Should().BeOfType<FailedProductSearchResult>();

            [Test]
            public void Should_indicate_the_reason_for_the_failure() =>
                ((FailedProductSearchResult) _result).Reason.Should().Be("There are no matching products.");
        }

        public class When_the_query_produces_a_runtime_exception : ProductSearcherSpecs
        {
            public override void Given() =>
                _productsQuery = ObjectMother.CreateProductQueryThatFails();

            [Test]
            public void Should_return_a_result() =>
                _result.Should().NotBeNull();

            [Test]
            public void Should_return_a_result_of_type_FailedProductSearchResult() =>
                _result.Should().BeOfType<FailedProductSearchResult>();

            [Test]
            public void Should_indicate_the_reason_for_the_failure() =>
                ((FailedProductSearchResult) _result).Reason.Should().Be("There was a problem, please try again.");
        }


        IFindProductsByTitleQuery _productsQuery;
        IProductSearchResult _result;

        static Text _someSearchText;
        static FoundProduct _someProduct = ObjectMother.CreateFoundProductWith(123);
        static FoundProduct _otherProduct = ObjectMother.CreateFoundProductWith(456);
    }
}