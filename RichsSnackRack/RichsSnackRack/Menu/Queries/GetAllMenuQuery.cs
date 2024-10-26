using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Menu
{
	public sealed record GetAllMenuQuery() : IRequest<IReadOnlyList<Snack>>;

    public sealed record GetAllMenuQueryHandler : IRequestHandler<GetAllMenuQuery, IReadOnlyList<Snack>>
    {
        private readonly SnackRackDbContext _snacksDbContext;

        public GetAllMenuQueryHandler(SnackRackDbContext snacksDbContext)
        {
            _snacksDbContext = snacksDbContext;
        }
        public async Task<IReadOnlyList<Snack>> Handle(GetAllMenuQuery query, CancellationToken cancellationToken)
            => await _snacksDbContext.Snacks
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken) ?? new List<Snack>();

    }
}

