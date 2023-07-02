using System;
using Humanizer;
using RichsSnackRack.Orders.Extensions;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Persistence;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models.Entities;

namespace RichsSnackRack.Orders
{
    public sealed class OrderRepository : IOrderRepository
	{
        private readonly SnackRackDbContext _snacksDbContext;
        private const string OrderDetailViewQuery = "SELECT od.Id, od.`Name`, od.Price, od.OrderTotal, od.OrderStatus, od.OrderDate FROM OrderDetails od";
        public OrderRepository(SnackRackDbContext snackRackDbContext)
		{
            _snacksDbContext = snackRackDbContext;
		}

        public async Task<Order> CreateOrder(Snack snack, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                SnackId = snack.Id,
                OrderStatus = Models.Enums.OrderStatus.Completed,
                OrderTotal = snack.Price
            };

            await _snacksDbContext.Orders.AddAsync(order, cancellationToken: cancellationToken);
            await _snacksDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return order;
        }

        public async Task<IReadOnlyList<OrderDetail>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await _snacksDbContext
                .OrderDetails
                .FromSqlRaw(OrderDetailViewQuery)
                .Select(order => order.ToDetail())
                .ToListAsync(cancellationToken: cancellationToken);
        }
        public async Task<OrderDetail> GetOrderDetailById(Guid id, CancellationToken cancellationToken)
        {
            OrderDetailEntity? orderDetailEntity = await _snacksDbContext
                .OrderDetails
                .FromSqlRaw(OrderDetailViewQuery)
                .FirstOrDefaultAsync(orderDetail => orderDetail.Id == id, cancellationToken);
            
            return orderDetailEntity is not null ? orderDetailEntity.ToDetail() : new OrderDetail()
            {
                Name = "Not Found",
                Price = 0,
                OrderStatus = OrderDetailStatus.Completed,
                OrderTotal = 0
            };
        }
    }
}

