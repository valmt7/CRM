namespace CRM.MidMiddleware;

public class NotFoundFleetsExeption : Exception
{
    public NotFoundFleetsExeption()
        : base($"Fleets not found.")
    {
    }
}