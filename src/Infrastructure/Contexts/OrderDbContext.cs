using Domain.Orders.Entities;
using Domain.Products.Entities;
using Infrastructure.Configurations.Orders;
using Infrastructure.Configurations.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}