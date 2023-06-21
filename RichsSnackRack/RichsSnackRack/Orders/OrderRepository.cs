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
        public async Task<OrderDetail> GetOrderDetailById(Guid id)
        {
            Order order = await _snacksDbContext.Orders.FindAsync(id);
            Snack snack = await _snacksDbContext.Snacks.FindAsync(order.SnackId);
            OrderDetail orderDetail = new()
            {
                Id = order.Id,
                Price = snack.Price,
                Name = snack.Name,
                OrderDate = order.OrderDate

            };

            return orderDetail;
        }
    }
}

