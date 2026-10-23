using Microsoft.EntityFrameworkCore;
using Prodcuts.Domain.Entities;
using Prodcuts.Domain.Interceptors;

namespace Prodcuts.Domain.AppContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // adding softDelete interceptor
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(eb =>
        {
            eb.HasKey(p => p.Id);
            eb.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            
            eb.Property(p => p.RowVersion)
                .IsRowVersion();

            eb.Property(p => p.Name)
                .HasMaxLength(1000)
                .IsRequired();

            eb.Property(p => p.Price)
                .IsRequired();

            // filter all soft deleted products    
            eb.HasQueryFilter(p => p.IsDeleted == false);

            eb.HasIndex(p => new
            {
                p.CategoryId,
                p.Price
            })
            .IncludeProperties(p => p.Name)
            .HasDatabaseName("IX_Products_CategoryId_Price");

            eb.HasIndex(p => new
            {
                p.CategoryId,
                p.Name
            })
            .IncludeProperties(p => p.Price)
            .HasDatabaseName("IX_Products_CategoryId_Name");
        });

        base.OnModelCreating(modelBuilder);
    }

}


public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        var Categories = new List<Category>
        {
            new Category().CreateNewCategory("Electronic Devices"),    
            new Category().CreateNewCategory("Electronic Accessories")
        };

        await db.AddRangeAsync(Categories);


        var products = new List<Product>
        {
            new Product().CreateNewProduct("Laptop", 30000, Categories[0]),
            new Product().CreateNewProduct("Smartphone", 15000, Categories[0]),
            new Product().CreateNewProduct("Tablet", 12000, Categories[0]),
            new Product().CreateNewProduct("Smartwatch", 5000, Categories[0]),
            new Product().CreateNewProduct("Wireless Headphones", 4000, Categories[1]),
            new Product().CreateNewProduct("Gaming Console", 18000, Categories[1]),
            new Product().CreateNewProduct("Bluetooth Speaker", 3500, Categories[1]),
            new Product().CreateNewProduct("External Hard ", 2500, Categories[1]),
            new Product().CreateNewProduct("Monitor", 7000, Categories[0]),
            new Product().CreateNewProduct("Keyboard", 1200, Categories[1]),
            new Product().CreateNewProduct("Mouse", 800, Categories[1]),
            new Product().CreateNewProduct("Printer", 6000, Categories[0]),
            new Product().CreateNewProduct("Router", 2000, Categories[0]),
            new Product().CreateNewProduct("Camera", 10000, Categories[1]),
            new Product().CreateNewProduct("Drone", 22000, Categories[0]),
            new Product().CreateNewProduct("VR Headset", 15000, Categories[1]),
            new Product().CreateNewProduct("Projector", 9000, Categories[1]),
            new Product().CreateNewProduct("Smart TV", 25000, Categories[0]),
            new Product().CreateNewProduct("Microwave", 8000),
            new Product().CreateNewProduct("Refrigerator", 3500),
        };

        await db.AddRangeAsync(products);

        await db.SaveChangesAsync();
    }
}