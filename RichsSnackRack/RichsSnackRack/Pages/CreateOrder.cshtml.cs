using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Menu;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Menu.Queries;
using RichsSnackRack.Orders.Commands;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Pages
{
	public class CreateOrderModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly Func<Snack?, PageResult> initPage;

        public CreateOrderModel(IMediator mediator)
        {
            _mediator = mediator;
            initPage = (Snack? snack) => { Snack = snack!; return Page(); };
        }
        [BindProperty]
        public Snack Snack { get; set; } = default!;
       
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            switch (id)
            {
                case null:
                    return RedirectToPage("./Index");
                default:
                    Snack? snack = await _mediator.Send(new GetSnackByIdQuery((int)id));
                    return snack is null ? RedirectToPage("./Index") : initPage(snack);
            }
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Snack is null || Snack == default)
            {
                ModelState.AddModelError(string.Empty, "Model Invalid");
                return Page();
            }
            Order order;
            try
            {
                order = await _mediator.Send(new CreateOrderCommand(Snack));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("./OrderConfirmation", new { id = order.Id });
        }
    }
}
