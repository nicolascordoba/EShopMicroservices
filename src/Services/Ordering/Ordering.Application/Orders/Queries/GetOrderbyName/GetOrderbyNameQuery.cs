namespace Ordering.Application.Orders.Queries.GetOrderbyName
{
    public record GetOrderbyNameQuery(string Name) : IQuery<GetOrderbyNameResult>;

    public record GetOrderbyNameResult(IEnumerable<OrderDto> Orders);
}
