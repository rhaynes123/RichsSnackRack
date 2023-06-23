using System;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Orders
{
	public interface IOrderRepository
	{
		Task<OrderDetail?> GetOrderDetailById(Guid id, CancellationToken cancellationToken = default);
		Task<IReadOnlyList<Order>> GetAllOrders(CancellationToken cancellationToken = default);
		Task<Order> CreateOrder(Snack snack, CancellationToken cancellationToken = default);
	}
}

