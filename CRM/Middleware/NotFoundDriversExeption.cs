namespace CRM.MidMiddleware
{

    public class NotFoundDriversExeption : Exception
    {
        public NotFoundDriversExeption()
            : base($"Drivers not found.")
        {
        }
    }
}