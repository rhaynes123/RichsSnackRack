﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Orders;
using RichsSnackRack.Orders.Queries;

namespace RichsSnackRack.Pages
{
	public class OrderDetailModel : PageModel
    {
        private readonly IMediator _mediator;
        public OrderDetailModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [BindProperty]
        public OrderDetail orderDetail { get; private set; } = default!;
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            orderDetail = await _mediator.Send(new GetOrderDetailQuery(id));
            return Page();
        }
    }
}
