using System;
using Webshop.Types;

namespace Webshop.Features.ProductRegistration
{
    public class ProductRegister
    {
        private readonly IProductStoringDAO _dao;

        public ProductRegister(IProductStoringDAO dao)
        {
            _dao = dao;
        }

        public IProductRegistrationResult Register(NewProductInputModel newProduct)
        {
            if (_dao.ThereIsAlreadySomeProductWith(newProduct.Number))
                return new FailedProductRegistrationResult("There is already another product that is using the same number.");
            if (_dao.ThereIsAlreadySomeProductWith(newProduct.Title))
                return new FailedProductRegistrationResult("There is already another product that is using the same title.");

            var product = new Product(newProduct.Number, Text.Create(newProduct.Title), newProduct.Price, Text.Create(newProduct.Description));
            StoredProduct storedProduct;
            try
            {
                storedProduct = _dao.Store(product);
            }
            catch (Exception)
            {
                return new FailedProductRegistrationResult("There was a problem, please try again."); // Of course here is the opportunity to log the specific exception
            }
            return new SuccessfulProductRegistrationResult(storedProduct);
        }
    }
}