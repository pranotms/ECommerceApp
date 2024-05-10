using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Services.Interfaces;
using ECommerceApp.Model;
using ECommerceApp.Services;
using Serilog;


public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
   

    public OrderItemService(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
        
    }

    public async Task<IEnumerable<OrderWithProduct>> GetAllUsersOrders()
    {
       
        var orders = await _orderItemRepository.GetAllUsersOrders();
        Log.Information("Retrieved  orders for all users");
        return orders;
    }

    public async Task<IEnumerable<OrderWithProduct>> GetUserOrders(int userId)
    {
        var orders = await _orderItemRepository.GetUserOrders(userId);
        Log.Information("Retrieved  orders for user with ID {UserId}", userId);
        return orders;
    }

    public async Task<object> PlaceOrder(OrderItem order)
    {
      
        await _orderItemRepository.InsertOrder(order);
        Log.Information("Order placed successfully");
        return order;
    }

    public async Task UpdateOrderStatus(int orderId, string newStatus)
    {
        await _orderItemRepository.UpdateOrderStatus(orderId, newStatus);
    }

    public async Task DeleteOrder(int orderId)
    {
        
        await _orderItemRepository.DeleteOrder(orderId);
        Log.Information("Deleted order with ID {OrderId}", orderId);
    }
}
