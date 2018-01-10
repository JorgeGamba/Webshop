using System.Collections.Concurrent;

namespace Webshop
{
    public class InMemoryStorage
    {
        public ConcurrentDictionary<int, StoredProduct> StoredProducts { get; } = new ConcurrentDictionary<int, StoredProduct>();

        public void Insert(StoredProduct product)
        {
            StoredProducts[product.Number] = product;
        }
    }
}