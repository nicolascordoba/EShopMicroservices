namespace Ordering.Application.Orders.Queries.GetOrderbyName
{
    public class GetOrderbyNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderbyNameQuery, GetOrderbyNameResult>
    {
        public async Task<GetOrderbyNameResult> Handle(GetOrderbyNameQuery query, CancellationToken cancellationToken)
        {
            //get orders by name using dbcontext
            //return result
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrderbyNameResult(orders.ToOrderDtoList());
        }
    }
}
