using System;
using MediatR;

namespace RichsSnackRack.Orders.Queries
{
	public sealed record GetOrderDetailQuery(Guid id) : IRequest<OrderDetail>;

	public sealed record GetOrderDetailQueryHandler: IRequestHandler<GetOrderDetailQuery, OrderDetail>
	{
        private readonly IOrderRepository _orderRepository;
		public GetOrderDetailQueryHandler(IOrderRepository orderRepository)
		{
            _orderRepository = orderRepository;
		}

        public async Task<OrderDetail> Handle(GetOrderDetailQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrderDetailById(query.id, cancellationToken);
        }
    }
}

