using ECommerceApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.ECommerce.Repositories.NewFolder
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
