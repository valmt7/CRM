using CRM.MidMiddleware;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services;

public class FleetService : IFleetService
{
    private readonly AppDbContext _context;
    private const int DEFAULT_FLEET_STATE = 100;
    private const int DEFAULT_FLEET_ROUTE = -1;
    private const int DEFAULT_FLEET_ORDER = -1;
    public FleetService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Fleet>> GetFleetAsync()
    {
        var fleets = await _context.Fleets.ToListAsync();
        if (fleets.Count == 0)
        {
            throw new NotFoundFleetsExeption();
        }
        return fleets;
    }

    public async Task<Fleet> CreateFleet(string name,string location)
    {
        var fleet = new Fleet
        {
            Name = name,
            Location = location,
            State = DEFAULT_FLEET_STATE,
            Route = DEFAULT_FLEET_ROUTE,
            OrderId = DEFAULT_FLEET_ORDER,


        };
        await _context.Fleets.AddAsync(fleet);
        await _context.SaveChangesAsync();
        return fleet;
    }

   
    public async Task<Fleet> UpdateFleetState(int id, int state)
    {
         
        var fleet = await _context.Fleets.FindAsync(id);
        if (fleet != null)
        {
            fleet.State = state;
            await _context.SaveChangesAsync();
        }
       
        return fleet;
    }

    public async Task<Fleet> SetFleetOrder(int id, int order)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        if (fleet == null)
        {
            return null;
        }
        fleet.OrderId = order;
        await _context.SaveChangesAsync();
        return fleet;
    }

    public async Task<string>  GetFleetlocation(int id)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        if (fleet == null)
        {
            return null;
        }
        return fleet?.Location;
    }

    public async Task<Fleet> UpdateFleetLocation(int id,string location)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        if (fleet == null)
        {
            return null;
        }
        fleet.Location = location;
        await _context.SaveChangesAsync();
        return fleet;
    }

    public async Task<Fleet> SetFleetRoute(int id, int route)
    {
        var fleet = await _context.Fleets.FindAsync(id);
        if (fleet == null)
        {
            return null;
        }
        fleet.Route = route;
        await _context.SaveChangesAsync();
        return fleet;
    }
}