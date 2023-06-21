using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mediator;
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
        public CreateOrderModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [BindProperty]
        public Snack snack { get; set; } = default!; 
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return RedirectToPage("./Index");
            }
            Snack? snack = await _mediator.Send(new GetSnackByIdQuery((int)id));
            if (snack is null)
            {
                return RedirectToPage("./Index");
            }
            this.snack = snack;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || snack is null || snack == default)
            {
                ModelState.AddModelError(string.Empty, "Model Invalid");
                return Page();
            }
            Order order;
            try
            {
                order = await _mediator.Send(new CreateOrderCommand(snack));
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
