using System;
using FluentAssertions;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Commands;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.IntegrationTests.Orders.Commands
{
	public class CreateOrderCommandTests: IClassFixture<IntegrationDbContextFixture>
	{
		private readonly IntegrationDbContextFixture _contextFixture;
		private readonly IOrderRepository orderRepository;
        public CreateOrderCommandTests(IntegrationDbContextFixture contextFixture)
		{
			_contextFixture = contextFixture;
			orderRepository = new OrderRepository(_contextFixture.dbContext);
        }

		[Fact]
		public async Task HandlerWasSuccessful()
		{
			//Arrange
			Snack snack = new()
			{
				Name = "Buffalo Wings",
				Price = 6.99m
			};
			
			CreateOrderCommand createCommand = new(snack);
			CreateOrderCommandHandler _sut = new(orderRepository);
            //Act
            var result = await _sut.Handle(createCommand, default);
			//Assert
			_contextFixture.dbContext
				.Orders
				.Should()
				.HaveCountGreaterThan(0);
			Assert.True(result.SnackId == snack.Id);
        }
    }
}

