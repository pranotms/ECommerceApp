

using ECommerceApp.Model;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItemsForUser(int userId)
        {
            try
            {
                var cartItems = await _cartService.GetCartItemsForUser(userId);
                if (cartItems.Count > 0)
                    return Ok(cartItems);
                else
                    return NotFound("User's cart is empty");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                var cartItems = await _cartService.GetAllCartItems();
                if (cartItems.Count > 0)
                    return Ok(cartItems);
                else
                    return NotFound("No cart items found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Cart cart)
        {
            try
            {
                var result = await _cartService.AddToCart(cart);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{cartId}")]
        public async Task<IActionResult> UpdateCartItem(int cartId, Cart cart)
        {
            try
            {
                var result = await _cartService.UpdateCartItem(cartId, cart);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            try
            {
                var result = await _cartService.DeleteCart(cartId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
