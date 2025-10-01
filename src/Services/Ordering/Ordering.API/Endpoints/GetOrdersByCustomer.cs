
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    //Accept a cusotmer ID parameter
    //Construct a GetOrdersByCustomerQuery to fetch data
    //Retrieves data that match with customer parameter

    //public record GetOrderByCustomerRequest(Guid CustomerId);//Lo saco de la URL
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
                .WithName("GetOrdersByCustomer")
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders By Customer")
                .WithDescription("Get Orders By Customer");
        }
    }
}
