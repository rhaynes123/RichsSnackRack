using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Orders.Models.Enums;
using RichsSnackRack.Sales.Queries;

namespace RichsSnackRack.IntegrationTests.Sales;

public class GetSalesQueryTests: IClassFixture<TestWebAppFactory>
{
    private readonly TestWebAppFactory _contextFixture;
    public GetSalesQueryTests(TestWebAppFactory contextFixture)
    {
        _contextFixture = contextFixture;
        
        contextFixture.SnackDbContext.Orders.AddRange(new List<Order>()
        {
            new Order
            {
                Id = Guid.NewGuid(),
                SnackId = contextFixture.SnackDbContext.Snacks.First().Id,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Completed,
                OrderTotal = contextFixture.SnackDbContext.Snacks.First().Price,
            },
        });

        contextFixture.SnackDbContext.SaveChanges();
    }
    [Fact]
    public async Task GetSalesTestAllSalesAreCompleted()
    {
        // Arrange
        GetSalesQueryHandler handler = new(_contextFixture.SnackDbContext);
        GetSalesQuery _sut = new GetSalesQuery();
        var orders = new List<Order>();
        // Act
        
        var results = await handler.Handle(_sut, CancellationToken.None);
        orders = results.SelectMany(result => result.Orders).ToList();
        // Assert
        Assert.True(orders.All(order => order.OrderStatus == OrderStatus.Completed));
    }
}