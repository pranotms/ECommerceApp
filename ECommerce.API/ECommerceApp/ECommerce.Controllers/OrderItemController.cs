using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Model;
using ECommerceAPI.ECommerce.Services.Interfaces;
using Microsoft.Extensions.Logging;

[Route("api/[controller]")]
[ApiController]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;
    private readonly ILogger<OrderItemController> _logger;

    public OrderItemController(IOrderItemService orderItemService, ILogger<OrderItemController> logger)
    {
        _orderItemService = orderItemService ?? throw new ArgumentNullException(nameof(orderItemService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // GET: api/orderitem/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<OrderWithProduct>>> GetAllUsersOrders()
    {
        try
        {
            var orders = await _orderItemService.GetAllUsersOrders();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all user orders.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    // GET: api/orderitem/{userId}
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<OrderWithProduct>>> GetUserOrders(int userId)
    {
        try
        {
            var orders = await _orderItemService.GetUserOrders(userId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching orders for user ID: {userId}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    // POST: api/orderitem/placeorder
    [HttpPost("placeorder")]
    public async Task<ActionResult<object>> PlaceOrder(OrderItem order)
    {
        try
        {
            var result = await _orderItemService.PlaceOrder(order);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while placing the order.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    // PUT: api/orderitem/updatestatus/{orderId}
    [HttpPut("updatestatus/{orderId}")]
    public async Task<ActionResult> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
    {
        try
        {
            await _orderItemService.UpdateOrderStatus(orderId, newStatus);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating the status for order ID: {orderId}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    // DELETE: api/orderitem/{orderId}
    [HttpDelete("{orderId}")]
    public async Task<ActionResult> DeleteOrder(int orderId)
    {
        try
        {
            await _orderItemService.DeleteOrder(orderId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the order with ID: {orderId}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}

