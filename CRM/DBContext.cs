using Microsoft.EntityFrameworkCore;

namespace CRM
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Fleet> Fleets { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        
        
    }

}