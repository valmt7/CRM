using CRM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CRM.Services
{
    public class ClientServices : IClientService
    {
        private readonly AppDbContext _context;

        public ClientServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Client>> GetClientAsync()
        {
            return await _context.Clients.ToListAsync();
        }
        public async Task<Client> MakeClient(string name, string phone, string email, string dostup)
        {
            var client = new Client
            {
                Name = name,
                Email = email,
                Access = dostup,
                Phone = phone,
                Likely = new List<string> { },
                Offers = new List<Products> {}
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> SetDostup(int id, string dostup)
        {
            var client = _context.Clients.Find(id);
            client.Access = dostup;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<List<Products>> GetOffers(int client_id)
        {
            var client = await _context.Clients
                .Include(c => c.Offers) 
                .FirstOrDefaultAsync(c => c.Id == client_id);
            return client?.Offers ?? new List<Products>();
        }

    }
}
