using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync();
        Task<int> CreateOrderDetailAsync(OrderDetail orderDetail);
    }
}