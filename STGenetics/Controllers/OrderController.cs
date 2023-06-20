using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Creates an order with the provided order details.
        /// </summary>
        /// <param name="order">The order details.</param>
        /// <returns>The order Id and the total purchase amount</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order([FromBody] OrderRequest order, CancellationToken cancellationToken)
        {
            int totalAmount = 0;
            float totalPrice = 0;

            try
            {
                if (order.Details != null)
                {
                    var orderId = await _orderService.CreateOrderAsync(cancellationToken);

                    foreach (var detail in order.Details)
                    {
                        var result = await _orderService.CalculateTotalPriceAsync(detail, cancellationToken);
                        totalAmount += result.Item1;
                        totalPrice += result.Item2;
                        await _orderService.CreateOrderDetailAsync(orderId, detail, cancellationToken);
                    }

                    return Ok(new { OrderId = orderId, TotalAmount = totalAmount, TotalPrice = totalPrice });
                }

                return StatusCode(400, "At least one order detail is required");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
