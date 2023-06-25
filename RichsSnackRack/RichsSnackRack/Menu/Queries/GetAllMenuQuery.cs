using System;
using Mediator;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Menu
{
	public sealed record GetAllMenuQuery() : IQuery<IReadOnlyList<Snack>>;

    public sealed record GetAllMenuQueryHandler : IQueryHandler<GetAllMenuQuery, IReadOnlyList<Snack>>
    {
        private readonly SnackRackDbContext _snacksDbContext;

        public GetAllMenuQueryHandler(SnackRackDbContext snacksDbContext)
        {
            _snacksDbContext = snacksDbContext;
        }
        public async ValueTask<IReadOnlyList<Snack>> Handle(GetAllMenuQuery query, CancellationToken cancellationToken)
            => await _snacksDbContext.Snacks
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

    }
}

