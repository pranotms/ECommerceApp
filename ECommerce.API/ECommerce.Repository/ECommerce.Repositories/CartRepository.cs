using Dapper;
using ECommerceAPI.ECommerce.Repositories;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceApp.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECommerceApp.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DapperContext _context;
      

        public CartRepository(DapperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            
        }
       
        public async Task<List<CartWithProduct>> GetCartItemsForUser(int userId)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                string query = "GetCartItemsForUserProcedure";
                var parameters = new { UserId = userId };

                return (await connection.QueryAsync<CartWithProduct>(query, parameters, commandType: CommandType.StoredProcedure)).AsList();
            }
        }
     
        public async Task<List<CartWithProduct>> GetAllCartItems()
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                string query = "GetAllCartItemsProcedure";

                return (await connection.QueryAsync<CartWithProduct>(query, commandType: CommandType.StoredProcedure)).AsList();
            }
        }
       

        public async Task AddToCart(Cart cart)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("AddToCartProcedure",
                    new { UserId = cart.UserId, ProductId = cart.ProductId, Quantity = cart.Quantity },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateCartItem(int cartId, Cart cart)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                var query = @"UpdateCartItemProcedure";
                var parameters = new { CartId = cartId, Quantity = cart.Quantity };

                await connection.ExecuteAsync(
                    query,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteCart(int cartId)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                var query = @"DeleteCartProcedure";
                var parameters = new { CartId = cartId };

                await connection.ExecuteAsync(
                    query,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
