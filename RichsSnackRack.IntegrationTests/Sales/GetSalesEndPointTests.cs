using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Orders.Models.Enums;

namespace RichsSnackRack.IntegrationTests.Sales;

public class GetSalesEndPointTests: IClassFixture<TestWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly TestWebAppFactory _factory;
    public GetSalesEndPointTests(TestWebAppFactory factory)
    {
        _client = factory.HttpClient;
        _factory = factory;
    }
    [Fact]
    public async Task GetAllSalesReturnsOkWith()
    {
        //Arrange & Act
        var response = await _client.GetAsync("/GetAllSales");
        //Assert
        Assert.True(response.IsSuccessStatusCode);
    }
    [Fact]
    public async Task GetAllSalesReturnsOkWithResults()
    {
        //Arrange & Act
        var snack = await _factory.SnackDbContext.Snacks.FirstAsync();
        await _factory.SnackDbContext.Orders.AddAsync(new Order
        {
            SnackId = snack.Id,
            Id = Guid.NewGuid(),
            OrderDate = DateTime.Now,
            OrderTotal = snack.Price,
            OrderStatus = OrderStatus.Completed
        });
        await _factory.SnackDbContext.SaveChangesAsync();
        var response = await _client.GetFromJsonAsync<IReadOnlyList<OrderDetail>>("/GetAllSales");
        //Assert
        Assert.True(response?.Any());
    }
}