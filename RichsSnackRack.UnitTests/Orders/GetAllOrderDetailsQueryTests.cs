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
        var orders = new List<OrderDetail>
        {
            new OrderDetail()
            {
                Name = "Not Found",
                Price = 0,
                OrderStatus = OrderDetailStatus.Completed,
                OrderTotal = 0
            }
        };

        mocRepo.Setup(mock => mock.GetAllOrders(It.IsAny<CancellationToken>())).ReturnsAsync(orders);

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
