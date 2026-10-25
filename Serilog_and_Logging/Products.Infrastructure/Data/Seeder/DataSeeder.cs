using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Domain.Entities;

namespace Products.Infrastructure.Data.Seeder;

public static class DataSeeder
{
    public static async Task SeedDataAsync(AppDbContext dbContext, ILogger logger)
    {
        var products = new Product[]
        {
            new Product() {Id=Guid.NewGuid(), Name="Laptop", Price=30000},
            new Product() {Id=Guid.NewGuid(), Name="Smartphone", Price=15000},
            new Product() {Id=Guid.NewGuid(), Name="Tablet", Price=12000},
            new Product() {Id=Guid.NewGuid(), Name="Smartwatch", Price=5000},
            new Product() {Id=Guid.NewGuid(), Name="Wireless Headphones", Price=4000},
            new Product() {Id=Guid.NewGuid(), Name="Gaming Console", Price=18000},
            new Product() {Id=Guid.NewGuid(), Name="Bluetooth Speaker", Price=3500},
            new Product() {Id=Guid.NewGuid(), Name="External Hard Drive", Price=2500},
            new Product() {Id=Guid.NewGuid(), Name="Monitor", Price=7000},
            new Product() {Id=Guid.NewGuid(), Name="Keyboard", Price=1200},
            new Product() {Id=Guid.NewGuid(), Name="Mouse", Price=800},
            new Product() {Id=Guid.NewGuid(), Name="Printer", Price=6000},
            new Product() {Id=Guid.NewGuid(), Name="Router", Price=2000},
            new Product() {Id=Guid.NewGuid(), Name="Camera", Price=10000},
            new Product() {Id=Guid.NewGuid(), Name="Drone", Price=22000},
            new Product() {Id=Guid.NewGuid(), Name="VR Headset", Price=15000},
            new Product() {Id=Guid.NewGuid(), Name="Projector", Price=9000},
            new Product() {Id=Guid.NewGuid(), Name="Smart TV", Price=25000},
            new Product() {Id=Guid.NewGuid(), Name="Microwave", Price=8000},
            new Product() {Id=Guid.NewGuid(), Name="Refrigerator", Price=35000}
        };

        await dbContext.Products.AddRangeAsync(products);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Products Seeded Successfully {data}", products);
    }
}