using System;
using MediatR;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Orders.Queries
{
    public sealed record GetAllOrderDetailsQuery() : IRequest<IReadOnlyList<OrderDetail>>;

    public sealed record GetAllOrderDetailsQueryHandler : IRequestHandler<GetAllOrderDetailsQuery, IReadOnlyList<OrderDetail>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrderDetailsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IReadOnlyList<OrderDetail>> Handle(GetAllOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllOrders(cancellationToken);
        }
    }
}

