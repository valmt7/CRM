using CRM.MidMiddleware;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services;

public class ManagerService : IManagerService
{
    public readonly AppDbContext _context;
    public ManagerService(AppDbContext  context)
    {
        _context =  context;
    }

    public async Task<IEnumerable<Manager>> GetAllManagers()
    {
        var managers = await _context.Managers.ToListAsync();
        if (managers.Count == 0)
        {
            throw new NotFoundManagersExeption();
        }
        return managers;
    }

    public async Task<Manager> AddManager(string managerName, string managerEmail, string managerLastName,
        string managerPhone)
    {
        var manager = new Manager
        {
            Name = managerName,
            Email = managerEmail,
            LastName = managerLastName,
            Phone = managerPhone
        };
        await _context.Managers.AddAsync(manager);
        await _context.SaveChangesAsync();
        return manager;
    }
}