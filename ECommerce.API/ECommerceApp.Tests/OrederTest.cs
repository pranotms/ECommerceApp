using ECommerceApp.Services;
using ECommerceApp.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ECommerceAPI.ECommerce.Repositories.NewFolder;

namespace ECommerceApp.Tests
{
    [TestFixture]
    public class OrderItemServiceTest
    {
        private OrderItemService _orderItemService;
        private Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private Mock<ILogger<OrderItemService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _loggerMock = new Mock<ILogger<OrderItemService>>();
            _orderItemService = new OrderItemService(_orderItemRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task TestGetAllUsersOrders()
        {
            // Arrange
            var expectedOrders = new List<OrderWithProduct>
            {
                new OrderWithProduct { ID = 1, UserID = 1, ProductID = 1, Quantity = 2, TotalPrice = 20.0m, OrderStatus = "Pending", Product = new Product { Id = 1, Name = "Product 1", UnitPrice = 10.0m, Quantity = 5 } },
                new OrderWithProduct { ID = 2, UserID = 1, ProductID = 2, Quantity = 1, TotalPrice = 15.0m, OrderStatus = "Pending", Product = new Product { Id = 2, Name = "Product 2", UnitPrice = 15.0m, Quantity = 3 } }
            };
            _orderItemRepositoryMock.Setup(repo => repo.GetAllUsersOrders()).ReturnsAsync(expectedOrders);

            // Act
            var result = await _orderItemService.GetAllUsersOrders();

            // Assert
            Assert.AreEqual(expectedOrders.Count, result.ToList().Count);
        }

        [Test]
        public async Task TestGetUserOrders()
        {
            // Arrange
            int userId = 1;
            var expectedOrders = new List<OrderWithProduct>
            {
                new OrderWithProduct { ID = 1, UserID = 1, ProductID = 1, Quantity = 2, TotalPrice = 20.0m, OrderStatus = "Pending", Product = new Product { Id = 1, Name = "Product 1", UnitPrice = 10.0m, Quantity = 5 } }
            };
             _orderItemRepositoryMock.Setup(repo => repo.GetUserOrders(userId)).ReturnsAsync(expectedOrders);



            // Act
            var result = await _orderItemService.GetUserOrders(userId);

            // Assert
            Assert.AreEqual(expectedOrders.Count, result.ToList().Count);

        }

        [Test]
        public async Task TestPlaceOrder()
        {
            // Arrange
            var order = new OrderItem { UserID = 1, ProductID = 1, Quantity = 2, TotalPrice = 20.0m, OrderStatus = "Pending" };

            // Act
            var result = await _orderItemService.PlaceOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(order, result);
        }

        [Test]
        public async Task TestUpdateOrderStatus()
        {
            // Arrange
            int orderId = 1;
            string newStatus = "Completed";

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _orderItemService.UpdateOrderStatus(orderId, newStatus));
        }

        [Test]
        public async Task TestDeleteOrder()
        {
            // Arrange
            int orderId = 1;

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _orderItemService.DeleteOrder(orderId));
        }
    }
}
