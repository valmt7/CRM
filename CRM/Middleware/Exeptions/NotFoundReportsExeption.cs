namespace CRM.MidMiddleware;

public class NotFoundReportsExeption : Exception
{
    public NotFoundReportsExeption()
        : base($"Reports not found.")
    {
    }
}