using System;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Menu.Queries;

namespace RichsSnackRack.IntegrationTests.Menu
{
	public class GetSnackByIdQueryTests: IClassFixture<TestWebAppFactory>
	{
		private readonly TestWebAppFactory _contextFixture;
		public GetSnackByIdQueryTests(TestWebAppFactory contextFixture)
		{
			_contextFixture = contextFixture;
            
        }

		[Fact]
		public async Task GetSnackByIdReturns()
		{
			//Arrange
			int lastSnack = _contextFixture.SnackDbContext.Snacks.OrderBy(snack => snack.Id).Last().Id;
            GetSnackByIdQuery query = new(lastSnack);
			GetSnackByIdQueryHandler _sut = new(_contextFixture.SnackDbContext);
			//Act
			var result = await _sut.Handle(query, default);
			//Assert
			Assert.True(result!.Price == 5.99m);
		}
	}
}

