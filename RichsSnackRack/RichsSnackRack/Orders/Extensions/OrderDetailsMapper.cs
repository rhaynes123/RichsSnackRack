
using RichsSnackRack.Orders.Models.Entities;

namespace RichsSnackRack.Orders.Extensions
{
	public static class OrderDetailsMapper
	{
		public static OrderDetail ToDetail(this OrderDetailEntity orderDetailEntity)
        {
            return new OrderDetail
			{
                Id = new OrderId(orderDetailEntity.Id),
                Price = orderDetailEntity.Price,
                Name = orderDetailEntity.Name,
                OrderStatus = orderDetailEntity.OrderStatus.ToOrderDetailStatus(),
                OrderTotal = orderDetailEntity.OrderTotal,
                OrderDate = orderDetailEntity.OrderDate
            };
		}
	}
}

