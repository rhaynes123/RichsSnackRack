﻿using System;
using Humanizer;
using RichsSnackRack.Orders.Extensions;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Persistence;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models.Entities;

namespace RichsSnackRack.Orders
{
    public class OrderRepository : IOrderRepository
	{
        private readonly SnackRackDbContext _snacksDbContext;
        public OrderRepository(SnackRackDbContext snackRackDbContext)
		{
            _snacksDbContext = snackRackDbContext;
		}

        public async Task<Order> CreateOrder(Snack snack, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                SnackId = snack.Id,
                OrderStatus = Models.Enums.OrderStatus.Completed
            };

            await _snacksDbContext.Orders.AddAsync(order, cancellationToken: cancellationToken);
            await _snacksDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await _snacksDbContext.Orders.ToListAsync();
        }
        public async Task<OrderDetail?> GetOrderDetailById(Guid id, CancellationToken cancellationToken)
        {
            OrderDetailEntity? orderDetailEntity = await _snacksDbContext
                .OrderDetails
                .FromSqlRaw("SELECT od.Id, od.`Name`, od.Price, od.OrderStatus, od.OrderDate FROM OrderDetails od")
                .FirstOrDefaultAsync(orderDetail => orderDetail.Id == id, cancellationToken);
            return orderDetailEntity is null ? null : orderDetailEntity.ToDetail();
        }
    }
}

