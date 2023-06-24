using System;
using Mediator;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Orders.Commands
{
	public sealed record CreateOrderCommand(Snack snack): IRequest<Order>;

    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepo;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepo = orderRepository;
        }
        public async ValueTask<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepo.CreateOrder(request.snack, cancellationToken);
        }
    }
}

