
using Dapper;
using ECommerceApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECommerceApp.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _configuration;

        public CartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<CartWithProduct>> GetCartItemsForUser(int userId)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var query = @"
                    SELECT c.*, p.Name AS ProductName, p.ImageUrl
                    FROM Cart c
                    INNER JOIN Products p ON c.ProductId = p.ID
                    WHERE c.UserId = @UserId
                ";
                return (await connection.QueryAsync<CartWithProduct>(query, new { UserId = userId })).AsList();
            }
        }

        public async Task<List<CartWithProduct>> GetAllCartItems()
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var query = @"
                    SELECT c.*, p.Name AS ProductName, p.ImageUrl
                    FROM Cart c
                    INNER JOIN Products p ON c.ProductId = p.ID
                ";
                return (await connection.QueryAsync<CartWithProduct>(query)).AsList();
            }
        }

        public async Task AddToCart(Cart cart)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var product = await connection.QueryFirstOrDefaultAsync<Product>("SELECT ID, Name, UnitPrice, Quantity, ImageUrl, Status FROM Products WHERE ID = @ProductId", new { cart.ProductId });
                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                decimal totalPrice = cart.Quantity * product.UnitPrice;

                await connection.ExecuteAsync(@"
                    INSERT INTO Cart (UserId, ProductId, UnitPrice, Quantity, TotalPrice)
                    VALUES (@UserId, @ProductId, @UnitPrice, @Quantity, @TotalPrice)",
                    new { cart.UserId, cart.ProductId, product.UnitPrice, cart.Quantity, totalPrice });
            }
        }

        public async Task UpdateCartItem(int cartId, Cart cart)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var cartItem = await connection.QueryFirstOrDefaultAsync<Cart>("SELECT ID, UserId, ProductId, UnitPrice, Quantity, TotalPrice FROM Cart WHERE ID = @Id", new { Id = cartId });
                if (cartItem == null)
                {
                    throw new Exception("Cart item not found");
                }

                cartItem.Quantity = cart.Quantity;
                cartItem.TotalPrice = cartItem.Quantity * cartItem.UnitPrice;

                await connection.ExecuteAsync(@"UPDATE Cart 
                    SET Quantity = @Quantity, TotalPrice = @TotalPrice
                    WHERE ID = @Id",
                    new { cartItem.Quantity, cartItem.TotalPrice, cartItem.Id });
            }
        }

        public async Task DeleteCart(int cartId)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var rowsAffected = await connection.ExecuteAsync("DELETE FROM Cart WHERE ID = @cartId", new { cartId = cartId });
                if (rowsAffected == 0)
                {
                    throw new Exception("Cart not found or already deleted");
                }
            }
        }
    }
}
