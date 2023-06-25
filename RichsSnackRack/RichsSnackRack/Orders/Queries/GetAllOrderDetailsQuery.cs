using System;
using Mediator;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Orders.Queries
{
    public sealed record GetAllOrderDetailsQuery() : IQuery<IReadOnlyList<OrderDetail>>;

    public sealed record GetAllOrderDetailsQueryHandler : IQueryHandler<GetAllOrderDetailsQuery, IReadOnlyList<OrderDetail>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrderDetailsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async ValueTask<IReadOnlyList<OrderDetail>> Handle(GetAllOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllOrders(cancellationToken);
        }
    }
}

