using System;
using NSubstitute;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Commands;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.UnitTests.Orders
{
	public class CreateOrderCommandTests
	{
        private readonly IOrderRepository mocRepo = Substitute.For<IOrderRepository>();
        private readonly Snack snack;
		private readonly Order order;
        public CreateOrderCommandTests()
		{
			snack = new Snack { Name = "Wing Dings", Price = 12.90m , Id = 2};
            order = new Order { OrderStatus = RichsSnackRack.Orders.Models.Enums.OrderStatus.NA, SnackId = snack.Id, OrderTotal = 0};
			mocRepo.CreateOrder(snack).Returns(order);

        }
		[Fact]
		public async Task CreateOrderReturnsOrder()
		{
			//Arrange
			var command = new CreateOrderCommand(snack);
            var handler = new CreateOrderCommandHandler(mocRepo);

			//Act

			var result = await handler.Handle(command, default);
			//Assert
			Assert.True(result.Id == order.Id);
        }
	}
}

