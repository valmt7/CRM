using Microsoft.EntityFrameworkCore;


namespace CRM.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Client>> GetClientAsync()
        {
            return await _context.Clients.ToListAsync();
        }
        public async Task<Client> MakeClient(string name, string phone, string email, string access)
        {
            var client = new Client
            {
                Name = name,
                Email = email,
                Access = access,
                Phone = phone,
                Likely = new List<string> { },
                Offers = new List<int>(){ },
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> SetClientAccess(int id, string access)
        {
            var client = _context.Clients.Find(id);
            client.Access = access;
            await _context.SaveChangesAsync();
            return client;
        }
        

        public async Task<List<Products>> GetOffers(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            var productList = client.Offers;
            var result = new List<Products>();
            for (int i = 0; i < productList.Count; i++)
            {
                result.Add(_context.Products.Find(productList[i]));
            }
            return result;
        }
        public async Task KillDataAsync()
        {
            _context.Clients.RemoveRange(_context.Clients);
            await _context.SaveChangesAsync();

        }

    }
}
