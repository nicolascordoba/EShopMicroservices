namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            //Update existing order
            //save db
            //return result

            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

            if (order == null)
                throw new OrderNotFoundException(command.Order.Id);

            UpdatreOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdatreOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updatedSippingAddress = Address.Of(orderDto.ShippingAdress.FirstName, orderDto.ShippingAdress.LastName, orderDto.ShippingAdress.EmailAddress, orderDto.ShippingAdress.AddressLine, orderDto.ShippingAdress.Country, orderDto.ShippingAdress.State, orderDto.ShippingAdress.ZipCode);
            var updatedBllingAddress = Address.Of(orderDto.BillingAdress.FirstName, orderDto.BillingAdress.LastName, orderDto.BillingAdress.EmailAddress, orderDto.BillingAdress.AddressLine, orderDto.BillingAdress.Country, orderDto.BillingAdress.State, orderDto.BillingAdress.ZipCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMehod);

            order.Update(OrderName.Of(orderDto.OrderName), updatedSippingAddress, updatedBllingAddress, updatedPayment, orderDto.Status);
        }
    }
}
