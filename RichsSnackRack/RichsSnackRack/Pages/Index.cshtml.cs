using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RichsSnackRack.Menu;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly SnacksDbContext _snacksDbContext;

    [BindProperty]
    public IReadOnlyList<Snack> snacks { get; set; } = new List<Snack>();
    public IndexModel(ILogger<IndexModel> logger
        , SnacksDbContext snacksDbContext)
    {
        _logger = logger;
        _snacksDbContext = snacksDbContext;
    }

    public void OnGet()
    {
        snacks = _snacksDbContext.Snacks.ToList().AsReadOnly();
    }
}

