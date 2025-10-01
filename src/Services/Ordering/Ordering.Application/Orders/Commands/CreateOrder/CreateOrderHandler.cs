using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //create Order entity from command object
            //save database
            //return result
            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);

            throw new NotImplementedException();
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = Address.Of(orderDto.ShippingAdress.FirstName, orderDto.ShippingAdress.LastName, orderDto.ShippingAdress.EmailAddress, orderDto.ShippingAdress.AddressLine, orderDto.ShippingAdress.Country, orderDto.ShippingAdress.State, orderDto.ShippingAdress.ZipCode);
            var billingAddress = Address.Of(orderDto.BillingAdress.FirstName, orderDto.BillingAdress.LastName, orderDto.BillingAdress.EmailAddress, orderDto.BillingAdress.AddressLine, orderDto.BillingAdress.Country, orderDto.BillingAdress.State, orderDto.BillingAdress.ZipCode);

            var newOrder = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customer: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMehod)
                );

            foreach (var orderItemDto in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }

            return newOrder;
        }
    }
}
