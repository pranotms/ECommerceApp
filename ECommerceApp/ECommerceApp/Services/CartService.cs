using ECommerceApp.Model;
using ECommerceApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _cartRepository.GetCartItemsForUser(userId);
        }

        public async Task<List<CartWithProduct>> GetAllCartItems()
        {
            return await _cartRepository.GetAllCartItems();
        }

        public async Task<string> AddToCart(Cart cart)
        {
            try
            {
                await _cartRepository.AddToCart(cart);
                return "Item added to cart successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add item to cart: " + ex.Message);
            }
        }

        public async Task<string> UpdateCartItem(int cartId, Cart cart)
        {
            try
            {
                await _cartRepository.UpdateCartItem(cartId, cart);
                return "Cart item updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update cart item: " + ex.Message);
            }
        }

        public async Task<string> DeleteCart(int cartId)
        {
            try
            {
                await _cartRepository.DeleteCart(cartId);
                return "Cart deleted successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete cart: " + ex.Message);
            }
        }
    }
}
