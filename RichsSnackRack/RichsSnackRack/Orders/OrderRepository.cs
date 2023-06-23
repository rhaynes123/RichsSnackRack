using System;
using Humanizer;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Persistence;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Menu.Models;

namespace RichsSnackRack.Orders
{
    public class OrderRepository : IOrderRepository
	{
        private readonly SnackRackDbContext _snacksDbContext;
        public OrderRepository(SnackRackDbContext snackRackDbContext)
		{
            _snacksDbContext = snackRackDbContext;
		}

        public async Task<IReadOnlyList<Order>> GetAllOrders()
        {
            return await _snacksDbContext.Orders.ToListAsync();
        }
        public async Task<OrderDetail?> GetOrderDetailById(Guid id, CancellationToken cancellationToken)
        {
            Order? order = await _snacksDbContext.Orders.FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
            if (order is null)
            {
                return default;
            }
            Snack? snack = await _snacksDbContext!.Snacks!.SingleOrDefaultAsync(snack => snack.Id == order.SnackId, cancellationToken)!;
            if(snack is null)
            {
                return default;
            }
            OrderDetail orderDetail = new()
            {
                Id = order.Id,
                Price = snack.Price,
                Name = snack.Name,
                OrderStatus = OrderDetailStatus.All.Single(status => status.Id == (int)order.OrderStatus),
                OrderDate = order.OrderDate
            };

            return orderDetail;
        }
    }
}

