using System;
using FluentAssertions;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Commands;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.IntegrationTests.Orders.Commands
{
	public class CreateOrderCommandTests: IClassFixture<IntegrationDbContextFixture>
	{
		private readonly IntegrationDbContextFixture _contextFixture;
        public CreateOrderCommandTests(IntegrationDbContextFixture contextFixture)
		{
			_contextFixture = contextFixture;
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
			CreateOrderCommandHandler _sut = new(_contextFixture.dbContext);
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

