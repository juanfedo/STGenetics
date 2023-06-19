using Dapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly string? _connectionString;

        public OrderRepository(IOptions<AppSettingsModel> settings)
        {
            this._connectionString = settings.Value.DbConnection;
        }
        public async Task<int> CreateOrderAsync()
        {
            using (SqlConnection con = new(_connectionString))
            {
                var sql = "INSERT INTO [Order] (TotalAmount, TotalPrice) VALUES (0, 0); SELECT CAST(SCOPE_IDENTITY() as int);";

                int newOrderId = (int)await con.ExecuteScalarAsync(sql);

                return newOrderId;
            }
        }

        public async Task<int> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var sql = "INSERT INTO [OrderDetail] (OrderId, AnimalId, Amount) VALUES (@OrderId, @AnimalId, @Amount); SELECT CAST(SCOPE_IDENTITY() as int);";

                var param = new
                {
                    orderDetail.OrderId,
                    orderDetail.AnimalId,
                    orderDetail.Amount,
                };

                var newOrderDetailId = (int)await con.ExecuteScalarAsync(sql, param);

                return newOrderDetailId;
            }
        }


    }
}
