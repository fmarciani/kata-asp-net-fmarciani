using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace AspNetCoreKata.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.QueryAsync<Product>("SELECT *, ProductId AS Id FROM product");
            }
        }

        public Product GetProduct(int id)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Query<Product>("SELECT *, ProductId AS Id FROM product WHERE ProductId = @Id").FirstOrDefault();
            }
        }

        public int DeleteProduct(int id)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Execute("DELETE FROM product WHERE ProductId = @Id", new { id });
            }
        }

        public int UpdateProduct(Product prod)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Execute("UPDATE product SET Name = @Name WHERE ProductID = @Id", prod);
            }
        }

        public int AddProduct(Product prod)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Execute("INSERT INTO product (Name) VALUES (@Name)", prod);
            }
        }
    }
}
