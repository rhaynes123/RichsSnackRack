using System;
using Mediator;
using RichsSnackRack.Orders.Models;

namespace RichsSnackRack.Orders.Queries
{
    public sealed record GetAllOrderDetailsQuery() : IQuery<IReadOnlyList<Order>>;

    public sealed class GetAllOrderDetailsQueryHandler : IQueryHandler<GetAllOrderDetailsQuery, IReadOnlyList<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrderDetailsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async ValueTask<IReadOnlyList<Order>> Handle(GetAllOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllOrders();
        }
    }
}

