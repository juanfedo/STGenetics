using Application.DTO;

namespace Application.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CancellationToken cancellationToken);
        Task<int> CreateOrderDetailAsync(int orderId, OrderDetailRequest orderDetail, CancellationToken cancellationToken);
        Task<(int, float)> CalculateTotalPriceAsync(OrderDetailRequest orderDetail, CancellationToken cancellationToken);
    }
}