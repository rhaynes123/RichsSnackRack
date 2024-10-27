using Microsoft.EntityFrameworkCore;
using MediatR;
using RichsSnackRack.Orders.Models.Enums;
using RichsSnackRack.Persistence;
using RichsSnackRack.Sales.Models;

namespace RichsSnackRack.Sales.Queries;

public sealed record GetSalesQuery(): IRequest<IReadOnlyList<Sale>>;

public sealed record GetSalesQueryHandler : IRequestHandler<GetSalesQuery, IReadOnlyList<Sale>>
{
    private readonly SnackRackDbContext _dbContext;

    public GetSalesQueryHandler(SnackRackDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<Sale>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
            .Where(order => order.OrderStatus == OrderStatus.Completed)
            .ToListAsync(cancellationToken);

        var snacks = await _dbContext.Snacks.ToDictionaryAsync(snack => snack.Id, cancellationToken);

        return orders
            .GroupBy(order => order.SnackId)
            .Select(group => new Sale
            {
                Snack = snacks[group.Key],  // Access snacks from in-memory dictionary
                Orders = group.ToList()
            })
            .ToList();

    }
};

