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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order([FromBody] OrderRequest order)
        {
            int totalAmount = 0;
            float totalPrice = 0;

            try
            {
                if (order.Details != null)
                {
                    var duplicateAnimals = order.Details.GroupBy(c => new {c.AnimalId})
                        .Where(g => g.Count() > 1)
                        .ToList();

                    if (duplicateAnimals.Any())
                    {
                        return StatusCode(400, "It's not allowed to duplicate the animal in the Order");
                    }

                    var orderId = await _orderService.CreateOrderAsync();

                    foreach (var detail in order.Details)
                    {
                        var result = await _orderService.CalculateTotalPriceAsync(detail);
                        totalAmount += result.Item1;
                        totalPrice += result.Item2;
                        await _orderService.CreateOrderDetailAsync(orderId, detail);
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
