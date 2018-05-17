using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ITB.Shared;
using Dapper;
using System.Threading.Tasks;

namespace ITB.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            using (var conn = _conn)
            {
                conn.Open();
                return await conn.QueryAsync<Product>("SELECT *, ProductId AS Id FROM product");
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return await conn.QueryFirstAsync<Product>("SELECT *, ProductId AS Id FROM product WHERE ProductId = @Id", new Product {Id = id});
            }
        }

        public int DeleteProduct(int id)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Execute("DELETE FROM product WHERE ProductId = @Id", new Product { Id = id });
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
