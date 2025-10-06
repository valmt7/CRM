namespace CRM.MidMiddleware
{
    public class NotFoundOrderByIdException : Exception
    {
        public NotFoundOrderByIdException(int orderId)
            : base($"Order with id {orderId} not found.")
        {
            OrderId = orderId;
        }
        public int OrderId { get; }
    }
}