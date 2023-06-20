using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Menu;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Commands;

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
            var snacks = await _mediator.Send(new GetAllMenuQuery());
            snack = snacks.FirstOrDefault(snack => snack.Id == id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || snack is null || snack == default)
            {
                ModelState.AddModelError(string.Empty, "Model Invalid");
                return Page();
            }

            try
            {
                await _mediator.Publish(new CreateOrderCommand(snack));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
