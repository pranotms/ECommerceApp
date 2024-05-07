using ECommerceApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.ECommerce.Repositories.NewFolder
{
    public interface ICartRepository
    {
        Task<List<CartWithProduct>> GetCartItemsForUser(int userId);
        Task<List<CartWithProduct>> GetAllCartItems();
        Task AddToCart(Cart cart);
        Task UpdateCartItem(int cartId, Cart cart);
        Task DeleteCart(int cartId);
    }
}
