using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ECommerceApp.Model;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using Microsoft.Extensions.Logging;
using ECommerceAPI.ECommerce.Repositories;

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
            using IDbConnection connection = _context.CreateConnection();
          
            var products = await connection.QueryAsync<Product>("GetProducts", commandType: CommandType.StoredProcedure);
            return products.AsList();
        }

      
        public async Task<Product> AddProduct(Product product)
        {
            using IDbConnection connection = _context.CreateConnection();
            string query = "InsertProductProcedure";
            var parameters = new
            {
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                Status = product.Status
            };

            int productId = await connection.QuerySingleAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
            product.Id = productId;

            return product;
        }



        public async Task<bool> UpdateProduct(int id, Product product)
        {
            using IDbConnection connection = _context.CreateConnection();
            string query = "UpdateProductProcedure";
            var parameters = new
            {
                Id = id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                Status = product.Status
            };

            var rowsAffected = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected > 0;
        }


        public async Task<bool> DeleteProduct(int id)
        {
            using IDbConnection connection = _context.CreateConnection();
            string query = "DeleteProductProcedure";
            var parameters = new { Id = id };

            var rowsAffected = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected > 0;
        }

        public async Task<List<Product>> SearchProducts(string query)
        {
            using IDbConnection connection = _context.CreateConnection();
            string queryString = "SELECT ID,Name,UnitPrice,Quantity,ImageUrl,Status FROM Products WHERE Name LIKE @Query";
            var parameters = new { Query = "%" + query + "%" }; 
            var products = await connection.QueryAsync<Product>(queryString, parameters);
            return products.AsList();
        }
        

    }
}
