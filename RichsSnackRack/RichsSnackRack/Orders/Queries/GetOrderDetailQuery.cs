using System;
using Mediator;

namespace RichsSnackRack.Orders.Queries
{
	public sealed record GetOrderDetailQuery(Guid id) : IQuery<OrderDetail>;

	public sealed record GetOrderDetailQueryHandler: IQueryHandler<GetOrderDetailQuery, OrderDetail?>
	{
        private readonly IOrderRepository _orderRepository;
		public GetOrderDetailQueryHandler(IOrderRepository orderRepository)
		{
            _orderRepository = orderRepository;
		}

        public async ValueTask<OrderDetail?> Handle(GetOrderDetailQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrderDetailById(query.id, cancellationToken);
        }
    }
}

