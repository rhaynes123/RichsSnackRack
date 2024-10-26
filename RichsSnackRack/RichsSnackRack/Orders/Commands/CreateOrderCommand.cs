using System;
using MediatR;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Persistence;

namespace RichsSnackRack.Orders.Commands
{
	public sealed record CreateOrderCommand(Snack snack): IRequest<Order>;

    public sealed record CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepo;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepo = orderRepository;
        }
        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepo.CreateOrder(request.snack, cancellationToken);
        }
    }
}

