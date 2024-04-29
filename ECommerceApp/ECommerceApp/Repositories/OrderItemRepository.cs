using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Model;
using ECommerceApp.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly string _connectionString;

    public OrderItemRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<OrderWithProduct>> GetAllUsersOrders()
    {
        using (IDbConnection connection = new SqlConnection(_connectionString))
        {
            var orderItems = await connection.QueryAsync<OrderWithProduct, Product, OrderWithProduct>(
                @"SELECT o.*, p.*
                  FROM Orders o
                  INNER JOIN Products p ON o.ProductID = p.ID",
                (order, product) =>
                {
                    order.Product = product;
                    return order;
                },
                splitOn: "ID");

            return orderItems;
        }
    }

    public async Task<IEnumerable<OrderWithProduct>> GetUserOrders(int userId)
    {
        using (IDbConnection connection = new SqlConnection(_connectionString))
        {
            var orderItems = await connection.QueryAsync<OrderWithProduct, Product, OrderWithProduct>(
                @"SELECT o.*, p.*
                          FROM Orders o
                          INNER JOIN Products p ON o.ProductID = p.ID
                          WHERE o.UserID = @UserId",
                (order, product) =>
                {
                    order.Product = product;
                    return order;
                },
                new { UserId = userId },
                splitOn: "ID");

            return orderItems;
        }
    }

   
    public async Task InsertOrder(OrderItem order)
    {
        using (IDbConnection connection = new SqlConnection(_connectionString))
        {
            
            var product = await connection.QueryFirstOrDefaultAsync<Product>(
                "SELECT ID, Name, UnitPrice, Quantity, ImageUrl, Status FROM Products WHERE ID = @ID",
                new { ID = order.ProductID });

            if (product == null)
            {
                throw new ArgumentException($"Product not found for ID: {order.ProductID}");
            }

           
            order.TotalPrice = product.UnitPrice * order.Quantity;

          
            order.OrderStatus = "Pending";

           
            await connection.ExecuteAsync(
                @"INSERT INTO Orders (UserID, ProductID, Quantity, TotalPrice, OrderStatus) 
                      VALUES (@UserID, @ProductID, @Quantity, @TotalPrice, @OrderStatus)",
                order);
        }
    }

    public async Task UpdateOrderStatus(int orderId, string newStatus)
    {
        using (IDbConnection connection = new SqlConnection(_connectionString))
        {
            await connection.ExecuteAsync(
                "UPDATE Orders SET OrderStatus = @NewStatus WHERE ID = @ID",
                new { NewStatus = newStatus, ID = orderId });
        }
    }

    public async Task DeleteOrder(int orderId)
    {
        using (IDbConnection connection = new SqlConnection(_connectionString))
        {
            await connection.ExecuteAsync(
                "DELETE FROM Orders WHERE ID = @ID",
                new { ID = orderId });
        }
    }
}

