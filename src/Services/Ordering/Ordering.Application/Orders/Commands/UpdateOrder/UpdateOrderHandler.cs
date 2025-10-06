﻿namespace Ordering.Application.Orders.Commands.UpdateOrder
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
            var updatedSippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var updatedBllingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMehod);

            order.Update(OrderName.Of(orderDto.OrderName), updatedSippingAddress, updatedBllingAddress, updatedPayment, orderDto.Status);
        }
    }
}
