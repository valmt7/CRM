namespace CRM.MidMiddleware;

public class NotFoundManagersExeption : Exception
{
    public NotFoundManagersExeption() : base($"Managers not found.")
    {
    }
}