namespace CRM.MidMiddleware
{
    public class NotFoundOrdersException : Exception
    {
        public NotFoundOrdersException()
            : base($"Orders not found.")
        {
            
        }
  }
}