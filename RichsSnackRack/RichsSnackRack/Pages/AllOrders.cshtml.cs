using System.Collections.Immutable;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Orders.Queries;

namespace RichsSnackRack.Pages
{
    public class AllOrdersModel : PageModel
    {
        private readonly IMediator _mediator;
        public AllOrdersModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [BindProperty]
        public IReadOnlyList<OrderDetail> orderDetails { get; private set; } = ImmutableList<OrderDetail>.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            var allOrders = await _mediator.Send(new GetAllOrderDetailsQuery());
            orderDetails = allOrders.OrderByDescending(order => order.OrderDate).ToList();
            return Page();
        }
    }
}
