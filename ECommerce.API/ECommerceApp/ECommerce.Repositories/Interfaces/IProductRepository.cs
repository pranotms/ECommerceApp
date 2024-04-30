using ECommerceApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.ECommerce.Repositories.NewFolder
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> AddProduct(Product product);
        Task<bool> UpdateProduct(int id, Product product);
        Task<bool> DeleteProduct(int id);
    }
}

