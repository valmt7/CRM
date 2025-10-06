namespace CRM.MidMiddleware;

public class NotFoundPrductsExeption : Exception
{
        public NotFoundPrductsExeption()
        : base($"Products not found.")
    {
    }
}