using System;
using Webshop.Features.ProductSearch;

namespace Webshop.UnitSpecs.Helpers
{
    public class ObjectMother
    {
        private static int _counter;

        internal static FoundProduct CreateFoundProductWith(int number) =>
            new FoundProduct(number, Guid.NewGuid().ToString(), _counter++);

        public static IFindProductsByTitleQuery CreateProductQueryThatReturns(params FoundProduct[] foundProducts) =>
            new FindProductsByTitleQueryFake(() => foundProducts);

        public static IFindProductsByTitleQuery CreateProductQueryThatFails() =>
            new FindProductsByTitleQueryFake(() => throw new Exception());
    }
}