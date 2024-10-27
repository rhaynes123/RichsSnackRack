
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Sales.Models;
public sealed record Sale
{
    public required Snack Snack { get; init; }
    public IEnumerable<Order> Orders { get; init; } = Enumerable.Empty<Order>();
};