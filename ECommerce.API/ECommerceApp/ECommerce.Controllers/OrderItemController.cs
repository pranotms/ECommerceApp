using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp.Model;
using ECommerceAPI.ECommerce.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
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
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
