using Moq;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Orders.Queries;

namespace RichsSnackRack.UnitTests;

public class GetAllOrderDetailsQueryTests
{
    private readonly Mock<IOrderRepository> mocRepo = new();

    public GetAllOrderDetailsQueryTests()
    {
        var orders = new List<Order>
        {
            new Order
            {
                SnackId = 1,
                OrderStatus = Orders.Models.Enums.OrderStatus.NA
            }
        };

        mocRepo.Setup(mock => mock.GetAllOrders()).ReturnsAsync(orders);

    }
    [Fact]
    public async Task HandlerReturnsData()
    {
        //Arrange
        GetAllOrderDetailsQuery query = new();
        GetAllOrderDetailsQueryHandler _sut = new(mocRepo.Object);
        //Act
        var orders = await _sut.Handle(query, default);
        //Assert
        Assert.NotEmpty(orders);
    }
}
