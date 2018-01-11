using System;
using Webshop.Types;

namespace Webshop.Features.ProductRegistration
{
    public class ProductRegister
    {
        public static IProductRegistrationResult Register(IProductStoringDAO productsDAO, NewProductInputModel newProduct)
        {
            if (productsDAO.ThereIsAlreadySomeProductWith(newProduct.Number))
                return new FailedProductRegistrationResult("There is already another product that is using the same number.");
            if (productsDAO.ThereIsAlreadySomeProductWith(newProduct.Title))
                return new FailedProductRegistrationResult("There is already another product that is using the same title.");

            var product = new Product(newProduct.Number, Text.Create(newProduct.Title), newProduct.Price, Text.Create(newProduct.Description));
            StoredProduct storedProduct;
            try
            {
                storedProduct = productsDAO.Store(product);
            }
            catch (Exception)
            {
                return new FailedProductRegistrationResult("There was a problem, please try again."); // Of course here is the opportunity to log the specific exception
            }
            return new SuccessfulProductRegistrationResult(storedProduct);
        }
    }
}