using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Services.Interfaces;
using ECommerceApp.Model;
using ECommerceApp.Services;
using Microsoft.Extensions.Logging;


public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ILogger<OrderItemService> _logger;

    public OrderItemService(IOrderItemRepository orderItemRepository, ILogger<OrderItemService> logger)
    {
        _orderItemRepository = orderItemRepository;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<OrderWithProduct>> GetAllUsersOrders()
    {
        return await _orderItemRepository.GetAllUsersOrders();
    }

    public async Task<IEnumerable<OrderWithProduct>> GetUserOrders(int userId)
    {
        return await _orderItemRepository.GetUserOrders(userId);
    }

    public async Task<object> PlaceOrder(OrderItem order)
    {
        await _orderItemRepository.InsertOrder(order);
        return order;
    }

    public async Task UpdateOrderStatus(int orderId, string newStatus)
    {
        await _orderItemRepository.UpdateOrderStatus(orderId, newStatus);
    }

    public async Task DeleteOrder(int orderId)
    {
        await _orderItemRepository.DeleteOrder(orderId);
    }
}
