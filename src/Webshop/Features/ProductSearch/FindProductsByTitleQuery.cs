using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Webshop.Types;

namespace Webshop.Features.ProductSearch
{
    public interface IFindProductsByTitleQuery
    {
        IEnumerable<FoundProduct> Execute(Text searchText);
    }

    public class FindProductsByTitleDbQuery : IFindProductsByTitleQuery
    {
        public IEnumerable<FoundProduct> Execute(Text searchText)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["WebshopDB"].ConnectionString))
            {
                var sql = "SELECT Number, Title, Price FROM Products WHERE Title LIKE @expression";
                var parameter = new { expression = $"%{searchText.Value}%" };
                return db.Query<FoundProduct>(sql, parameter).ToList();
            }
        }
    }

    public class FindProductsByTitleMemoryQuery : IFindProductsByTitleQuery
    {
        private readonly InMemoryStorage _storage;

        public FindProductsByTitleMemoryQuery(InMemoryStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<FoundProduct> Execute(Text searchText) =>
            _storage.StoredProducts
                    .Values
                    .Where(x => x.Title.Contains(searchText.Value))
                    .Select(x => new FoundProduct(x.Number, x.Title, x.Price));
    }
}