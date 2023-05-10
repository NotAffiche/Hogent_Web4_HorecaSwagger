using HorecaSwagger.DLEF.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF;

public class DataContext : DbContext
{
    private string _connectionString;

    public DataContext(string conn)
    {
        _connectionString = conn;
    }

    public DbSet<CustomerEF> Customers { get; set; }
    public DbSet<DishEF> Dishes { get; set; }
    public DbSet<OrderEF> Orders { get; set; }
    public DbSet<OrderDetailsEF> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
        var connString = _connectionString;
        optionsBuilder.UseMySql(connString, serverVersion).LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEF>()
            .HasIndex(c => c.Email).IsUnique();
        modelBuilder.Entity<CustomerEF>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerUUID)
            .HasPrincipalKey(c => c.CustomerUUID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderEF>()
            .HasMany(o => o.Dishes)
            .WithMany(d => d.Orders)
            .UsingEntity<OrderDetailsEF>
            (
            l => l.HasOne<DishEF>().WithMany().HasForeignKey(od => od.DishUUID).OnDelete(DeleteBehavior.Restrict),
            r => r.HasOne<OrderEF>().WithMany().HasForeignKey(od => od.OrderUUID).OnDelete(DeleteBehavior.Restrict)
            );
    }
}
