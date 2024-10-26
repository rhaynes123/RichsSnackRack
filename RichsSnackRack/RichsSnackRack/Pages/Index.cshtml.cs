using System.Collections.Immutable;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Menu;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly IMediator _mediator;

    [BindProperty]
    public IReadOnlyList<Snack> snacks { get; set; } = ImmutableList<Snack>.Empty;
    public IndexModel(ILogger<IndexModel> logger
        , IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGet()
    {
        snacks = await _mediator.Send(new GetAllMenuQuery());
        return Page();
    }
}

