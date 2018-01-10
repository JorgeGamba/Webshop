namespace Webshop.Features.ProductRegistration
{
    public interface IProductRegistrationResult
    {
    }

    public class SuccessfulProductRegistrationResult : IProductRegistrationResult
    {
        public SuccessfulProductRegistrationResult(StoredProduct storedProduct)
        {
            StoredProduct = storedProduct;
        }

        public StoredProduct StoredProduct { get; }
    }

    public class FailedProductRegistrationResult : IProductRegistrationResult
    {
        public FailedProductRegistrationResult(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; }
    }
}