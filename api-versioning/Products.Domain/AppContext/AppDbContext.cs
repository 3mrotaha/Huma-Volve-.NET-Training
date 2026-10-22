using Microsoft.EntityFrameworkCore;
using Prodcuts.Domain.Entities;

namespace Prodcuts.Domain.AppContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(eb =>
        {
            eb.HasKey(p => p.Id);

            eb.Property(p => p.Name)
                .HasMaxLength(1000)
                .IsRequired();

            eb.Property(p => p.Price)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

}


public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        var products = new List<Product>
        {
            new Product() {Id=1, Name="Laptop", Price=30000},
            new Product() {Id=2, Name="Smartphone", Price=15000},
            new Product() {Id=3, Name="Tablet", Price=12000},
            new Product() {Id=4, Name="Smartwatch", Price=5000},
            new Product() {Id=5, Name="Wireless Headphones", Price=4000},
            new Product() {Id=6, Name="Gaming Console", Price=18000},
            new Product() {Id=7, Name="Bluetooth Speaker", Price=3500},
            new Product() {Id=8, Name="External Hard Drive", Price=2500},
            new Product() {Id=9, Name="Monitor", Price=7000},
            new Product() {Id=10, Name="Keyboard", Price=1200},
            new Product() {Id=11, Name="Mouse", Price=800},
            new Product() {Id=12, Name="Printer", Price=6000},
            new Product() {Id=13, Name="Router", Price=2000},
            new Product() {Id=14, Name="Camera", Price=10000},
            new Product() {Id=15, Name="Drone", Price=22000},
            new Product() {Id=16, Name="VR Headset", Price=15000},
            new Product() {Id=17, Name="Projector", Price=9000},
            new Product() {Id=18, Name="Smart TV", Price=25000},
            new Product() {Id=19, Name="Microwave", Price=8000},
            new Product() {Id=20, Name="Refrigerator", Price=35000}
        };


        await db.AddRangeAsync(products);

        await db.SaveChangesAsync();
    }
}