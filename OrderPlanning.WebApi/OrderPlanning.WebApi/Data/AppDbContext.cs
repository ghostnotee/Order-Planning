using Microsoft.EntityFrameworkCore;
using OrderPlanning.WebApi.Models;

namespace OrderPlanning.WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(ob => { ob.HasKey(o => o.OrderId); });
        base.OnModelCreating(modelBuilder);
    }
}