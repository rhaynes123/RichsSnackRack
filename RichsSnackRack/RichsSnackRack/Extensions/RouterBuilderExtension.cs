using Microsoft.AspNetCore.Http.HttpResults;
using RichsSnackRack.Orders;

namespace RichsSnackRack.Extensions;

public static class RouterBuilderExtension
{
    public static void MapSalesEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("GetAllSales", GetAllSalesOrders);
    }

    public static async Task<Results<Ok<IReadOnlyList<OrderDetail>>, ProblemHttpResult>> GetAllSalesOrders(IOrderRepository orderRepository)
    {
        try
        {
            var orders = await orderRepository.GetAllOrders();
            return TypedResults.Ok(orders);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: "Error");
        }
        
    }
}