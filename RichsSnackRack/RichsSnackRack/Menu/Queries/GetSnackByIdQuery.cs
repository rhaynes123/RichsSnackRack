using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Menu.Queries
{
    public sealed record GetSnackByIdQuery(int id) : IRequest<Snack?>;

    public record class GetSnackByIdQueryHandler : IRequestHandler<GetSnackByIdQuery, Snack?>
    {
        private readonly SnackRackDbContext _snacksDbContext;
        public GetSnackByIdQueryHandler(SnackRackDbContext snackRackDbContext)
        {
            _snacksDbContext = snackRackDbContext;
        }
        /// <summary>
        /// Returns a Single Snack From the Menu With No Tracking On. Might Return Null If the Item Can't Be Found
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Snack?> Handle(GetSnackByIdQuery query, CancellationToken cancellationToken)
        {
            return await _snacksDbContext
                .Snacks
                .AsNoTracking()
                .FirstOrDefaultAsync(snack => snack.Id == query.id, cancellationToken: cancellationToken);
        }
    }
}

