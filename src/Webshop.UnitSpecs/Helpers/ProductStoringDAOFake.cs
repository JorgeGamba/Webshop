using System;
using Webshop.Features.ProductRegistration;

namespace Webshop.UnitSpecs.Helpers
{
    public class ProductStoringDAOFake : IProductStoringDAO
    {
        private readonly bool _numberIsAlreadyUsed;
        private readonly bool _titleIsAlreadyUsed;
        private Func<Product, StoredProduct> _storeFunc;

        public ProductStoringDAOFake(bool numberIsAlreadyUsed, bool titleIsAlreadyUsed, bool storingFails = false)
        {
            _numberIsAlreadyUsed = numberIsAlreadyUsed;
            _titleIsAlreadyUsed = titleIsAlreadyUsed;
            if (storingFails)
                _storeFunc = product => throw new Exception();
            else
                _storeFunc = product => new StoredProduct(product.Number, product.Title.Value, product.Price, product.Description.Value);
        }

        public bool ThereIsAlreadySomeProductWith(int productNumber) => _numberIsAlreadyUsed;

        public bool ThereIsAlreadySomeProductWith(string productTitle) => _titleIsAlreadyUsed;

        public StoredProduct Store(Product product) => _storeFunc(product);
    }   
}