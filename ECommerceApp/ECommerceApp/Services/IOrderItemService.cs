using ECommerceApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderWithProduct>> GetAllUsersOrders();
        Task<IEnumerable<OrderWithProduct>> GetUserOrders(int userId);
        Task<object> PlaceOrder(OrderItem order);
        Task UpdateOrderStatus(int orderId, string newStatus);
        Task DeleteOrder(int orderId);
    }
}

