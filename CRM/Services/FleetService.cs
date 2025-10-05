using CRM;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services;

public class FleetService : IFleetService
{
    private readonly AppDbContext _context;

    public FleetService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Fleet>> GetFleetAsync()
    {
        return await _context.Fleets.ToListAsync();
    }

    public async Task<Fleet> CreateFleet(string name,string location)
    {
        var fleet = new Fleet
        {
            Name = name,
            Location = location,
            State = 100,
            Route = -1,
            OrderId = -1,


        };
        await _context.Fleets.AddAsync(fleet);
        await _context.SaveChangesAsync();
        return fleet;
    }

   
    public async Task<Fleet> UpdateFleetState(int id, int state)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        fleet.State = state;
        await _context.SaveChangesAsync();
        return fleet;
    }

    public async Task<Fleet> SetFleetOrder(int id, int order)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        fleet.OrderId = order;
        await _context.SaveChangesAsync();
        return fleet;
    }

    public async Task<string>  GetFleetlocation(int id)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        return fleet?.Location;
    }

    public async Task<Fleet> UpdateFleetLocation(int id,string location)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        fleet.Location = location;
        await _context.SaveChangesAsync();
        return fleet;
    }

    public async Task<Fleet> SetFleetRoute(int id, int route)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        fleet.Route = route;
        await _context.SaveChangesAsync();
        return fleet;
    }
}