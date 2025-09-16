namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        //OrderId and ProductId are strongly type ID
        //Defined to avoid wrong order when pass information parameters to the constructor
        //constructor is internal due to the creation of the OrderItems depends on the Aggregation Order who manage the status of the Order and OrderItems
        //OrderItem should be created only by agregate of the order (IMPORTANT)
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
        public OrderId OrderId { get; private set; } = default!;
        public ProductId ProductId { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}
