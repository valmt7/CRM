namespace CRM.MidMiddleware;

public class NotFoundClientsExeption : Exception
    {
        public NotFoundClientsExeption()
            : base($"Clients not found.")
        {
        }
    }
