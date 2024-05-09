using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Services.Interfaces;
using ECommerceApp.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
    public async Task<List<Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }

    public async Task<Product> AddProduct(Product product)
    {
        return await _productRepository.AddProduct(product);
    }

    public async Task<bool> UpdateProduct(int id, Product product)
    {
        return await _productRepository.UpdateProduct(id, product);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        return await _productRepository.DeleteProduct(id);
    }

        public async Task<List<Product>> SearchProducts(string query)
        {
           
            return await _productRepository.SearchProducts(query);
        }

    }
}

