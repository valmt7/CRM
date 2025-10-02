﻿using Microsoft.EntityFrameworkCore;

namespace CRM
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Order> Orders { get; set; }
    }
}