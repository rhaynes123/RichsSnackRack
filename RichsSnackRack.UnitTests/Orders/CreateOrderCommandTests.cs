using System;
using Moq;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Commands;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.UnitTests.Orders
{
	public class CreateOrderCommandTests
	{
        private readonly Mock<IOrderRepository> mocRepo = new();
		private readonly Snack snack;
		private readonly Order order;
        public CreateOrderCommandTests()
		{
			snack = new Snack { Name = "Wing Dings", Price = 12.90m , Id = 2};
            order = new Order { OrderStatus = RichsSnackRack.Orders.Models.Enums.OrderStatus.NA, SnackId = snack.Id, };
			mocRepo.Setup(repo => repo.CreateOrder(snack,It.IsAny<CancellationToken>())).ReturnsAsync(order);
        }
		[Fact]
		public async Task CreateOrderReturnsOrder()
		{
			//Arrange
			var command = new CreateOrderCommand(snack);
            var handler = new CreateOrderCommandHandler(mocRepo.Object);

			//Act

			var result = await handler.Handle(command, default);
			//Assert
			Assert.True(result.Id == order.Id);
        }
	}
}

