using ECommerceAPI.ECommerce.Services.Interfaces;
using ECommerceApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetProducts();
                if (products.Count > 0)
                {
                    return Ok(products);
                }
                else
                {
                    return Ok("Product table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products");
                return NotFound(ex.Message);
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                var addedProduct = await _productService.AddProduct(product);
                return Ok("Product added successfully: " + addedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding product");
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            try
            {
                var isUpdated = await _productService.UpdateProduct(id, product);
                if (isUpdated)
                {
                    return Ok("Product updated successfully");
                }
                else
                {
                    return Ok("Product with this Id does not exist");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product");
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var isDeleted = await _productService.DeleteProduct(id);
                if (isDeleted)
                {
                    return Ok("Product deleted successfully");
                }
                else
                {
                    return Ok("Product with this id does not exist");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product");
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Product/search?query=searchTerm
        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProducts(string query)
        {
            try
            {
                var products = await _productService.SearchProducts(query);
                if (products.Count > 0)
                {
                    return Ok(products);
                }
                else
                {
                    return Ok("No products found matching the search criteria");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching products");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

    }
}
