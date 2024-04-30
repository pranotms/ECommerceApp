using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Repositories;
using Microsoft.Extensions.Logging; 

namespace ECommerceApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<ProductRepository> _logger;

        
        public ProductRepository(DapperContext context, ILogger<ProductRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                var products = await connection.QueryAsync<Product>("SELECT id, Name, UnitPrice, Quantity, ImageUrl, Status FROM Products");
                return products.AsList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products");
                throw;
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                string query = @"INSERT INTO Products (Name, UnitPrice, Quantity, ImageUrl, Status) 
                                 VALUES (@Name, @UnitPrice, @Quantity, @ImageUrl, @Status);
                                 SELECT CAST(SCOPE_IDENTITY() as int)";
                int productId = await connection.QuerySingleAsync<int>(query, product);
                product.Id = productId;

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding product");
                throw;
            }
        }

        public async Task<bool> UpdateProduct(int id, Product product)
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                string query = @"UPDATE Products 
                                 SET Name = @Name, UnitPrice = @UnitPrice, Quantity = @Quantity, ImageUrl = @ImageUrl, Status = @Status
                                 WHERE ID = @Id";
                product.Id = id;
                var rowsAffected = await connection.ExecuteAsync(query, product);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product");
                throw;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                string query = @"DELETE FROM Products WHERE ID = @Id";
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product");

                throw;
            }
        }
    }
}
