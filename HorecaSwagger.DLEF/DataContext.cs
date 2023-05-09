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
        modelBuilder.Entity<OrderEF>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<DishEF>()
        .HasMany(d => d.Orders)
        .WithMany(o => o.Dishes)
        .UsingEntity<OrderDetailsEF>(
            "OrderDetails",
            j => j
                .HasOne<OrderEF>()
                .WithMany()
                .HasForeignKey("OrderUUID")
                .OnDelete(DeleteBehavior.Restrict),
            j => j
                .HasOne<DishEF>()
                .WithMany()
                .HasForeignKey("DishUUID")
                .OnDelete(DeleteBehavior.Restrict),
            j =>
            {
                j.HasKey(od => od.UUID);
                j.ToTable("OrderDetails");
            });
    }
}
