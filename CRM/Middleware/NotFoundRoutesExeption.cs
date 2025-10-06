namespace CRM.MidMiddleware;

public class NotFoundRoutesExeption : Exception
{   
    public NotFoundRoutesExeption()
    : base($"Routes not found.")
{
}
}