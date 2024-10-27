using RichsSnackRack.Orders.Extensions;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Persistence;
using RichsSnackRack.Orders.Models;
using RichsSnackRack.Menu.Models;
using RichsSnackRack.Orders.Models.Entities;

namespace RichsSnackRack.Orders
{
    public sealed class OrderRepository(SnackRackDbContext snackRackDbContext) : IOrderRepository
    {
        private static readonly FormattableString OrderDetailViewQuery = @$"SELECT 
        od.Id
     , od.`Name`
     , od.Price
     , od.OrderTotal
     , od.OrderStatus
     , od.OrderDate 
            FROM OrderDetails od";

        private IQueryable<OrderDetailEntity> OrderDetailsQuery()
        {
            return snackRackDbContext
                .Database.SqlQuery<OrderDetailEntity>(OrderDetailViewQuery);
        }

        public async Task<Order> CreateOrder(Snack snack, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                SnackId = snack.Id,
                OrderStatus = Models.Enums.OrderStatus.Completed,
                OrderTotal = snack.Price
            };

            await snackRackDbContext.Orders.AddAsync(order, cancellationToken: cancellationToken);
            await snackRackDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return order;
        }

        public async Task<IReadOnlyList<OrderDetail>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await OrderDetailsQuery().Select(order => order.ToDetail())
                .ToListAsync(cancellationToken: cancellationToken);
        }
        public async Task<OrderDetail> GetOrderDetailById(Guid id, CancellationToken cancellationToken)
        {
            OrderDetailEntity? orderDetailEntity = await OrderDetailsQuery()
                .FirstOrDefaultAsync(orderDetail => orderDetail.Id == id, cancellationToken);
            
            return orderDetailEntity is not null ? orderDetailEntity.ToDetail() : new OrderDetail()
            {
                Name = "Not Found",
                Price = 0,
                OrderStatus = OrderDetailStatus.Completed,
                OrderTotal = 0
            };
        }
    }
}

