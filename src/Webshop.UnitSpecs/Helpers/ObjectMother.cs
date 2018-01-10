using System;
using Webshop.Features.ProductRegistration;
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

        public static NewProductInputModel CreateNewProductInputModelWith(int number = 1, string title = "any title", decimal price = 2, string description = "any description") =>
            new NewProductInputModel { Number = number, Title = title, Price = price, Description = description };
    }
}