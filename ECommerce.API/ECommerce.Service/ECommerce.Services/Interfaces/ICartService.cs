using ECommerceApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.ECommerce.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartWithProduct>> GetCartItemsForUser(int userId);
        Task<List<CartWithProduct>> GetAllCartItems();
        Task<string> AddToCart(Cart cart);
        Task<string> UpdateCartItem(int cartId, Cart cart);
        Task<string> DeleteCart(int cartId);
    }
}

