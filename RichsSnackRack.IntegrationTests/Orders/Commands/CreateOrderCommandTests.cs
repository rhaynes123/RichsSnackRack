using System;
using FluentAssertions;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Commands;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.IntegrationTests.Orders.Commands
{
	public class CreateOrderCommandTests: IClassFixture<TestWebAppFactory>
	{
		private readonly TestWebAppFactory testWebAppFactory;
		private IOrderRepository orderRepository;
        public CreateOrderCommandTests(TestWebAppFactory WebAppFactory)
		{
			testWebAppFactory = WebAppFactory;
            orderRepository = new OrderRepository(testWebAppFactory.SnackDbContext);
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
			testWebAppFactory.SnackDbContext
				.Orders
				.Should()
				.HaveCountGreaterThan(0);
			Assert.True(result.SnackId == snack.Id);
        }
    }
}

