using ECommerceApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderWithProduct>> GetAllUsersOrders();
        Task<IEnumerable<OrderWithProduct>> GetUserOrders(int userId);
        Task InsertOrder(OrderItem order);
        Task UpdateOrderStatus(int orderId, string newStatus);
        Task DeleteOrder(int orderId);
    }
}
