
using ECommerceApp.Model;
using ECommerceApp.Repositories;
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
    }
}
