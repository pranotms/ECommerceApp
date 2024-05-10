using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Services.Interfaces;
using ECommerceApp.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            
        }
    
    public async Task<List<Product>> GetProducts()
    {
        var product= await _productRepository.GetProducts();
        Log.Information("Retrieved all the count of products");
        return product;
    }

    public async Task<Product> AddProduct(Product product)
        {
            var addedProduct = await _productRepository.AddProduct(product);
            Log.Information("Added product with ID {ProductId}", addedProduct.Id);
            return addedProduct;
        }

    public async Task<bool> UpdateProduct(int id, Product product)
    {
            var isUpdated = await _productRepository.UpdateProduct(id, product);
            if (isUpdated)
            {
                Log.Information("Updated product with ID {ProductId}", id);
            }
            else
            {
                Log.Information("Product with ID {ProductId} not found for update", id);
            }
            return isUpdated;
        }

    public async Task<bool> DeleteProduct(int id)
    {
        return await _productRepository.DeleteProduct(id);
    }

        public async Task<List<Product>> SearchProducts(string query)
        {

            var products = await _productRepository.SearchProducts(query);
            Log.Information("Searched products with query: {Query}, found {Count} products", query, products.Count);
            return products;
        }

    }
}

