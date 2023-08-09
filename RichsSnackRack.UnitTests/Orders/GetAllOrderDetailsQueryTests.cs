using NSubstitute;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Orders.Queries;

namespace RichsSnackRack.UnitTests;

public class GetAllOrderDetailsQueryTests
{
    private readonly IOrderRepository mocRepo = Substitute.For<IOrderRepository>();

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

        mocRepo.GetAllOrders(cancellationToken: Arg.Any<CancellationToken>()).Returns(orders);

    }
    [Fact]
    public async Task HandlerReturnsData()
    {
        //Arrange
        GetAllOrderDetailsQuery query = new();
        GetAllOrderDetailsQueryHandler _sut = new(mocRepo);
        //Act
        var orders = await _sut.Handle(query, default);
        //Assert
        Assert.NotEmpty(orders);
    }
}
