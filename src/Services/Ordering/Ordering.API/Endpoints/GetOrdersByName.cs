
using Ordering.Application.Orders.Queries.GetOrderbyName;

namespace Ordering.API.Endpoints
{
    //Accept a name as a parameter
    //Construct a GetOrdersByNameQuery
    //Retrieves data that match with parameter

    //public record GetOrdersByNameRequest(string Name);//Lo scamos de la URL
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderbyNameQuery(orderName));

                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            })
                .WithName("GetOrdersByName")
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders By Name")
                .WithDescription("Get Orders By Name");
        }
    }
}
