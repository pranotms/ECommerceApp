using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Data;
using ECommerceApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return false;

            existingProduct.Name = product.Name;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.Quantity = product.Quantity;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Status = product.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
