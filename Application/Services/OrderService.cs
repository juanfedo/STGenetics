using Application.DTO;
using Application.Entities;
using Application.Extensions;
using Domain.Models;
using Infrastructure.Repositories;

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

        public async Task<int> CreateOrderAsync(CancellationToken cancellationToken) =>
            await _orderRepository.CreateOrderAsync(cancellationToken);

        public async Task<int> CreateOrderDetailAsync(int orderId, OrderDetailRequest orderDetail, CancellationToken cancellationToken) =>
            await _orderRepository.CreateOrderDetailAsync(new OrderDetail()
            {
                OrderId = orderId,
                Amount = orderDetail.Amount,
                AnimalId = orderDetail.AnimalId
            }, cancellationToken);

        public async Task<(int, float)> CalculateTotalPriceAsync(OrderDetailRequest orderDetail, CancellationToken cancellationToken)
        {
           var price = await _animalRepository.PriceByAnimalAsync(orderDetail.AnimalId, cancellationToken);
           return (orderDetail.Amount, price)
                .Pipe(Discounts.QuantityDiscount)
                .Pipe(Discounts.FreightDiscount);
        }
    }
}
