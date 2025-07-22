using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RichsSnackRack.Extensions;
using RichsSnackRack.Orders;

namespace RichsSnackRack.UnitTests.Extensions;

public class RouterBuilderExtensionTests
{
    
    private readonly IOrderRepository mocRepo = Substitute.For<IOrderRepository>();

    [Fact]
    public async Task MapRoutes_MapsGetSalesReturn200WithSalesOrders()
    {
        //Arrange
        mocRepo.GetAllOrders(cancellationToken: Arg.Any<CancellationToken>()).Returns([
            new OrderDetail
            {
                Name = "Not Found",
                Price = 0,
                OrderStatus = OrderDetailStatus.Completed,
                OrderTotal = 0
            }
        ]);
        //Act
        var response = await RouterBuilderExtension.GetAllSalesOrders(mocRepo);
        //Assert
        
        Assert.IsType<Results<Ok<IReadOnlyList<OrderDetail>>, ProblemHttpResult>>(response);

        var okResult = (Ok<IReadOnlyList<OrderDetail>>)response.Result;
        Assert.True(okResult.Value?.Count > 0);
    }
    
    [Fact]
    public async Task MapRoutes_MapsGetSalesReturns500WhenExceptionIsThrown()
    {
        //Arrange
        mocRepo.GetAllOrders(cancellationToken: Arg.Any<CancellationToken>()).ThrowsAsync(new Exception());
        //Act
        var response = await RouterBuilderExtension.GetAllSalesOrders(mocRepo);
        //Assert
        
        Assert.IsType<Results<Ok<IReadOnlyList<OrderDetail>>, ProblemHttpResult>>(response);

        var problemResults = (ProblemHttpResult)response.Result;
        Assert.Equal("Error", problemResults.ProblemDetails.Detail);
    }
}