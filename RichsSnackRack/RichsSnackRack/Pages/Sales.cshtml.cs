using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Menu.Queries;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Sales.Queries;

namespace RichsSnackRack.Pages;

public sealed record Sale
{
    public Snack snack { get; init; }
    public IEnumerable<Order> Orders { get; init; } = Enumerable.Empty<Order>();
};
public class Sales : PageModel
{
    private readonly IMediator _mediator;
    public Dictionary<Snack, IEnumerable<Order>> SoldOrders { get; set; }

    public Sales(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IActionResult> OnGet()
    {
        var sales = await _mediator.Send(new GetSalesQuery());
        var sold = sales.GroupBy(s => s.SnackId)
            .Select(async s => 
                new Sale { 
                    snack = await _mediator.Send(new GetSnackByIdQuery(s.Key))
                    ,Orders = s.Select(v => v)
                });
        List<Sale> results = new();
        foreach (var s in sold)
        {
            results.Add(await s);
        }
        SoldOrders = results.ToDictionary(result => result.snack, result => result.Orders);
        return Page();
    }
}