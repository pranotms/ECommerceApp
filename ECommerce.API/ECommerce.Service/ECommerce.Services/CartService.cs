using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Services.Interfaces;
using ECommerceApp.Model;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ECommerceApp.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
          
        }

        public async Task<List<CartWithProduct>> GetCartItemsForUser(int userId)
        {
            var cartItems = await _cartRepository.GetCartItemsForUser(userId);
            Log.Information("Retrieved {Count} cart items for user with ID {UserId}", cartItems.Count, userId);
            return cartItems;
        }

        public async Task<List<CartWithProduct>> GetAllCartItems()
        {
            var cartItems = await _cartRepository.GetAllCartItems();
            Log.Information("Retrieved {Count} cart items", cartItems.Count);
            return cartItems;
        }

        public async Task<string> AddToCart(Cart cart)
        {
            
                await _cartRepository.AddToCart(cart);
                Log.Information("Item added to cart successfully");
                return "Item added to cart successfully";
           
        }

        public async Task<string> UpdateCartItem(int cartId, Cart cart)
        {
            
                await _cartRepository.UpdateCartItem(cartId, cart);
                Log.Information("Cart item updated successfully");
                return "Cart item updated successfully";
           
        }

        public async Task<string> DeleteCart(int cartId)
        {
           
                await _cartRepository.DeleteCart(cartId);
                Log.Information("Cart deleted successfully");
                return "Cart deleted successfully";
            
          
        }
    }
}

