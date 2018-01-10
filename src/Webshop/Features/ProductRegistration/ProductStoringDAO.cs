using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Webshop.Features.ProductRegistration
{
    public interface IProductStoringDAO
    {
        bool ThereIsAlreadySomeProductWith(int productNumber);
        bool ThereIsAlreadySomeProductWith(string productTitle);
        StoredProduct Store(Product product);
    }

    public class ProductStoringDbDao : IProductStoringDAO
    {
        public bool ThereIsAlreadySomeProductWith(int productNumber)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["WebshopDB"].ConnectionString))
            {
                var exists = db.ExecuteScalar<bool>("SELECT COUNT(1) FROM Products WHERE Number=@productNumber", new { productNumber });
                return exists;
            }
        }

        public bool ThereIsAlreadySomeProductWith(string productTitle)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["WebshopDB"].ConnectionString))
            {
                var exists = db.ExecuteScalar<bool>("SELECT COUNT(1) FROM Products WHERE Title=@productTitle", new { productTitle });
                return exists;
            }
        }

        public StoredProduct Store(Product product)
        {
            var sql = @"INSERT INTO Products (Number, Title, Price, Description)
                        values (@Number, @Title, @Price, @Description);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new { product.Number, Title = product.Title.Value, product.Price, Description = product.Description.Value };
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["WebshopDB"].ConnectionString))
            {
                var id = connection.Query<int>(sql, parameters).Single();
                return new StoredProduct(id, product.Number, product.Title.Value, product.Price, product.Description.Value);
            }
        }
    }

    public class ProductStoringMemoryDao : IProductStoringDAO
    {
        private readonly InMemoryStorage _storage;

        public ProductStoringMemoryDao(InMemoryStorage storage)
        {
            _storage = storage;
        }

        public bool ThereIsAlreadySomeProductWith(int productNumber) =>
            _storage.StoredProducts.Values.Any(p => p.Number == productNumber);

        public bool ThereIsAlreadySomeProductWith(string productTitle) =>
            _storage.StoredProducts.Values.Any(p => p.Title == productTitle);

        public StoredProduct Store(Product product)
        {
            var nextAvailableStoredId = _storage.StoredProducts.Any() 
                ? _storage.StoredProducts.Values.Max(x => x.Id) + 1 
                : 1;
            var storedProduct = new StoredProduct(nextAvailableStoredId, product.Number, product.Title.Value, product.Price, product.Description.Value);
            _storage.StoredProducts[storedProduct.Number] = storedProduct;

            return storedProduct;
        }
    }
}