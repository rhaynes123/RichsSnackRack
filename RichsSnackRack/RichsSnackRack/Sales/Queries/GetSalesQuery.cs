using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Orders.Models.Enums;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Sales.Queries;
using MediatR;
public sealed record GetSalesQuery(): IRequest<IReadOnlyList<Order>>;

public sealed record GetSalesQueryHandler : IRequestHandler<GetSalesQuery, IReadOnlyList<Order>>
{
    private readonly SnackRackDbContext _dbContext;

    public GetSalesQueryHandler(SnackRackDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<Order>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Where(ord => ord.OrderStatus == OrderStatus.Completed)
            .ToListAsync(cancellationToken);
    }
};

