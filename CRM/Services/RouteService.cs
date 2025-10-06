using CRM.MidMiddleware;
using Microsoft.EntityFrameworkCore;
namespace CRM.Services;

public class RouteService : IRouteService
{
  
        private readonly AppDbContext _context;

        public RouteService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Route>> GetRouteAsync()
        {
            var routes = await _context.Routes.ToListAsync();
            if (routes.Count == 0)
            {
                throw new NotFoundRoutesExeption();
            }
            return routes; 
        }
        
        
}