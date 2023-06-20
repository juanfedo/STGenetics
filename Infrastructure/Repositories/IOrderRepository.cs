using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(CancellationToken cancellationToken);
        Task<int> CreateOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken);
    }
}