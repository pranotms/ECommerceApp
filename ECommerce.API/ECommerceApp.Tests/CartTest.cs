using ECommerceApp.Services;
using ECommerceApp.Model;
using Moq;
using Microsoft.Extensions.Logging;
using ECommerceAPI.ECommerce.Repositories.NewFolder;

namespace ECommerceApp.Tests
{
    [TestFixture]
    public class CartTest
    {
        private CartService _cartService;
        private Mock<ICartRepository> _cartRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            var loggerMock = new Mock<ILogger<CartService>>();
            _cartService = new CartService(_cartRepositoryMock.Object, loggerMock.Object);
        }

        [Test]
        public async Task TestGetCartItemsForUser()
        {
            // Arrange
            int userId = 1;
            var expectedCartItems = new List<CartWithProduct> { new CartWithProduct { ID = 1, UserId = userId, ProductId = 1, Quantity = 2 },
                                                                new CartWithProduct { ID = 2, UserId = userId, ProductId = 2, Quantity = 1 } };
            _cartRepositoryMock.Setup(repo => repo.GetCartItemsForUser(userId)).ReturnsAsync(expectedCartItems);

            // Act
            var result = await _cartService.GetCartItemsForUser(userId);

            // Assert
            Assert.AreEqual(expectedCartItems.Count, result.Count);
        }

        [Test]
        public async Task TestAddToCart()
        {
            // Arrange
            var newCartItem = new Cart { Id = 1, UserId = 1, ProductId = 1, Quantity = 2 };

            _cartRepositoryMock.Setup(repo => repo.AddToCart(It.IsAny<Cart>())).Returns(Task.CompletedTask);

            // Act
            var result = await _cartService.AddToCart(newCartItem);

            // Assert
            Assert.AreEqual("Item added to cart successfully", result);
        }

        [Test]
        public async Task TestUpdateCartItem()
        {
            // Arrange
            int cartId = 1;
            var updatedCartItem = new Cart { Id = cartId, UserId = 1, ProductId = 1, Quantity = 3 };

            _cartRepositoryMock.Setup(repo => repo.UpdateCartItem(cartId, It.IsAny<Cart>())).Returns(Task.CompletedTask);

            // Act
            var result = await _cartService.UpdateCartItem(cartId, updatedCartItem);

            // Assert
            Assert.AreEqual("Cart item updated successfully", result);
        }

        [Test]
        public async Task TestDeleteCart()
        {
            // Arrange
            int cartId = 1;

            _cartRepositoryMock.Setup(repo => repo.DeleteCart(cartId)).Returns(Task.CompletedTask);

            // Act
            var result = await _cartService.DeleteCart(cartId);

            // Assert
            Assert.AreEqual("Cart deleted successfully", result);
        }
    }
}
