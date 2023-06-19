using Application.DTO;
using Application.Entities;
using Application.Extensions;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _orderRepository;
        readonly IAnimalRepository _animalRepository;

        public OrderService(IOrderRepository orderRepository, IAnimalRepository animalRepository)
        {
            this._orderRepository = orderRepository;
            this._animalRepository = animalRepository;
        }

        public async Task<int> CreateOrderAsync() =>
            await _orderRepository.CreateOrderAsync();

        public async Task<int> CreateOrderDetailAsync(int orderId, OrderDetailRequest orderDetail) =>
            await _orderRepository.CreateOrderDetailAsync(new OrderDetail()
            {
                OrderId = orderId,
                Amount = orderDetail.Amount,
                AnimalId = orderDetail.AnimalId
            });

        public async Task<(int, float)> CalculateTotalPriceAsync(OrderDetailRequest orderDetail)
        {
           var price = await _animalRepository.PriceByAnimalAsync(orderDetail.AnimalId);
           return (orderDetail.Amount, price)
                .Pipe(Discounts.QuantityDiscount)
                .Pipe(Discounts.BulkDiscount);
        }
    }
}
