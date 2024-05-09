
using Dapper;
using System.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Model;
using ECommerceAPI.ECommerce.Repositories;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceApp.Repositories;
using Microsoft.Extensions.Logging;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly DapperContext _context;
    private readonly ILogger<OrderItemRepository> _logger;

    public OrderItemRepository(DapperContext context, ILogger<OrderItemRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    
    public async Task<IEnumerable<OrderWithProduct>> GetAllUsersOrders()
    {
        using (IDbConnection connection = _context.CreateConnection())
        {
            var orderItems = await connection.QueryAsync<OrderWithProduct, Product, OrderWithProduct>(
                "GetAllUsersOrders",
                (order, product) =>
                {
                    order.Product = product;
                    return order;
                },
                splitOn: "ID",
                commandType: CommandType.StoredProcedure);

            return orderItems;
        }
    }


  
    public async Task<IEnumerable<OrderWithProduct>> GetUserOrders(int userId)
    {
        using (IDbConnection connection = _context.CreateConnection())
        {
            var orderItems = await connection.QueryAsync<OrderWithProduct, Product, OrderWithProduct>(
                "GetUserOrders", 
                (order, product) =>
                {
                    order.Product = product;
                    return order;
                },
                new { UserId = userId },
                splitOn: "ID",
                commandType: CommandType.StoredProcedure);

            return orderItems;
        }
    }


   

    public async Task InsertOrder(OrderItem order)
    {
        using (IDbConnection connection = _context.CreateConnection())
        {
            var parameters = new
            {
                UserID = order.UserID,
                ProductID = order.ProductID,
                Quantity = order.Quantity
            };

            await connection.ExecuteAsync(
                "InsertOrder", 
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }

    public async Task UpdateOrderStatus(int orderId, string newStatus)
    {
        using (IDbConnection connection = _context.CreateConnection())
        {
            var parameters = new
            {
                OrderId = orderId,
                NewStatus = newStatus
            };

            await connection.ExecuteAsync(
                "UpdateOrderStatus",  
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DeleteOrder(int orderId)
    {
        using (IDbConnection connection = _context.CreateConnection())
        {
            var parameters = new { OrderId = orderId };
            await connection.ExecuteAsync("DeleteOrder", parameters, commandType: CommandType.StoredProcedure);
        }
    }

}
