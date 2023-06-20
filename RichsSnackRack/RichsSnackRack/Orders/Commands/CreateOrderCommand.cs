using System;
using Mediator;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Orders.Commands
{
	public sealed record CreateOrderCommand(Snack snack): INotification;

    public sealed class CreateOrderCommandHandler : INotificationHandler<CreateOrderCommand>
    {
        private readonly SnackRackDbContext _snacksDbContext;

        public CreateOrderCommandHandler(SnackRackDbContext snackRackDbContext)
        {
            _snacksDbContext = snackRackDbContext;
        }
        public async ValueTask Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                SnackId = request.snack.Id
            };

            await _snacksDbContext.Orders.AddAsync(order, cancellationToken:cancellationToken);
            await _snacksDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        }
    }
}

