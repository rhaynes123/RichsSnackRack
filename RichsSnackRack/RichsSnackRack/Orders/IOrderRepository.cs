using System;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Orders
{
	public interface IOrderRepository
	{
		Task<OrderDetail?> GetOrderDetailById(Guid id, CancellationToken cancellationToken = default);
		Task<IReadOnlyList<Order>> GetAllOrders();
	}
}

