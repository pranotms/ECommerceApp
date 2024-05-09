
using ECommerceApp.Services;
using ECommerceApp.Model;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using System.Collections.Generic;

namespace ECommerceApp.Tests
{
    [TestFixture]
    public class ProductTest
    {
        private ProductService _productService;
        private Mock<IProductRepository> _productRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            var loggerMock = new Mock<ILogger<ProductService>>();
            _productService = new ProductService(_productRepositoryMock.Object, loggerMock.Object);
        }

        [Test]
        public async Task TestGetProducts()
        {
            // Arrange
            var expectedProducts = new List<Product> { new Product { Id = 1, Name = "Product 1", UnitPrice = 10.0m, Quantity = 5 },
                                                      new Product { Id = 2, Name = "Product 2", UnitPrice = 15.0m, Quantity = 3 } };
            _productRepositoryMock.Setup(repo => repo.GetProducts()).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetProducts();

            // Assert
            
            Assert.AreEqual(expectedProducts.Count, result.Count);
            
        }

        [Test]
        public async Task TestAddProduct()
        {
            // Arrange
            var newProduct = new Product { Id = 3, Name = "New Product", UnitPrice = 20.0m, Quantity = 8 };

            _productRepositoryMock.Setup(repo => repo.AddProduct(It.IsAny<Product>())).ReturnsAsync(newProduct);

            // Act
            var result = await _productService.AddProduct(newProduct);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newProduct.Id, result.Id);
        }

        [Test]
        public async Task TestUpdateProduct()
        {
            // Arrange
            int productId = 1;
            var updatedProduct = new Product { Id = productId, Name = "Updated Product", UnitPrice = 25.0m, Quantity = 10 };

            _productRepositoryMock.Setup(repo => repo.UpdateProduct(productId, It.IsAny<Product>())).ReturnsAsync(true);

            // Act
            var result = await _productService.UpdateProduct(productId, updatedProduct);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestDeleteProduct()
        {
            // Arrange
            int productId = 2;

            _productRepositoryMock.Setup(repo => repo.DeleteProduct(productId)).ReturnsAsync(true);

            // Act
            var result = await _productService.DeleteProduct(productId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
