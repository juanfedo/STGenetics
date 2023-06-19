using Domain.Models;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync();
        Task<int> CreateOrderDetailAsync(OrderDetail orderDetail);
    }
}