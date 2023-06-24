
using RichsSnackRack.Orders.Models.Entities;

namespace RichsSnackRack.Orders.Extensions
{
	public static class OrderDetailsMapper
	{
		public static OrderDetail ToDetail(this OrderDetailEntity orderDetailEntity)
        {
            return new OrderDetail
			{
                Id = orderDetailEntity.Id,
                Price = orderDetailEntity.Price,
                Name = orderDetailEntity.Name,
                OrderStatus = OrderDetailStatus.All.Single(status => status.Id == (int)orderDetailEntity.OrderStatus),
                OrderDate = orderDetailEntity.OrderDate
            };
		}
	}
}

