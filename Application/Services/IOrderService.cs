using Application.DTO;

namespace Application.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync();
        Task<int> CreateOrderDetailAsync(int orderId, OrderDetailRequest orderDetail);
        Task<(int, float)> CalculateTotalPriceAsync(OrderDetailRequest orderDetail);
    }
}