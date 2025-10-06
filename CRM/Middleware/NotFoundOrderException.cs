namespace CRM.MidMiddleware
{
    public class NotFoundOrderException : Exception
    {
        public NotFoundOrderException(int orderId)
            : base($"Order with id {orderId} not found.")
        {
            OrderId = orderId;
        }
        public int OrderId { get; }
    }
}