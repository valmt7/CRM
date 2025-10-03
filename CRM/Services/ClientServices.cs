using CRM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                Dostup = dostup,
                Phone = phone,
                Likely = new List<string>() { "None" }
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
    }
}
