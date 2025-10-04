using CRM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IClientService
    {
       Task<IEnumerable<Client>> GetClientAsync();
       Task<Client> MakeClient(string name, string phone, string email, string access);
       Task<Client> SetClientAccess(int id, string access);
       Task<List<Products>> GetOffers(int client_id);
    }
}