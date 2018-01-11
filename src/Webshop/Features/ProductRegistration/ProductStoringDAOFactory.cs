namespace Webshop.Features.ProductRegistration
{
    public class ProductStoringDAOFactory
    {
        private readonly InMemoryStorage _inMemoryStorage;

        public ProductStoringDAOFactory(InMemoryStorage inMemoryStorage)
        {
            _inMemoryStorage = inMemoryStorage;
        }

        public IProductStoringDAO Create(bool workingOnMemory)
        {
            if (workingOnMemory)
                return new ProductStoringMemoryDao(_inMemoryStorage);

            return new ProductStoringDbDao();
        }
    }
}