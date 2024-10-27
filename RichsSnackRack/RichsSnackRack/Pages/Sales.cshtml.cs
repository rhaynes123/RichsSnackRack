using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Menu.Queries;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Sales.Models;
using RichsSnackRack.Sales.Queries;

namespace RichsSnackRack.Pages;


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
        
        SoldOrders = sales.ToDictionary(result => result.Snack, result => result.Orders);
        return Page();
    }
}