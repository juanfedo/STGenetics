using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace STGenetics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Order([FromBody] OrderRequest order)
        {
            int totalAmount = 0;
            float totalPrice = 0;
            var orderId = await _orderService.CreateOrderAsync();

            try
            {
                if (order.Details != null)
                {
                    foreach (var detail in order.Details)
                    {
                        var result = await _orderService.CalculateTotalPriceAsync(detail);
                        totalAmount += result.Item1;
                        totalPrice += result.Item2;
                        await _orderService.CreateOrderDetailAsync(orderId, detail);
                    }
                }
                return Ok(new { OrderId = orderId, TotalAmount = totalAmount, TotalPrice = totalPrice });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
