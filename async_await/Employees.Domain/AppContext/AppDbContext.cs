using Microsoft.EntityFrameworkCore;
using Employees.Domain.Entities;

namespace Employees.Domain.AppContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(eb =>
        {
            eb.HasKey(e => e.Id);

            eb.Property(e => e.Name)
                .HasMaxLength(1000)
                .IsRequired();

            eb.Property(e => e.Salary)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

}


public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken ct)
    {
        var employees = new List<Employee>
        {
            new Employee() {Id=1, Name="Ahmed Ali", Salary=15000},
            new Employee() {Id=2, Name="Mohamed Hassan", Salary=12000},
            new Employee() {Id=3, Name="Sara Mahmoud", Salary=18000},
            new Employee() {Id=4, Name="Omar Khaled", Salary=9000},
            new Employee() {Id=5, Name="Mona Adel", Salary=11000},
            new Employee() {Id=6, Name="Youssef Ibrahim", Salary=20000},
            new Employee() {Id=7, Name="Nour Samir", Salary=8500},
            new Employee() {Id=8, Name="Karim Tarek", Salary=13000},
            new Employee() {Id=9, Name="Hana Mostafa", Salary=16000},
            new Employee() {Id=10, Name="Mahmoud Fathy", Salary=10000},
            new Employee() {Id=11, Name="Laila Sayed", Salary=9500},
            new Employee() {Id=12, Name="Hassan Nabil", Salary=17000},
            new Employee() {Id=13, Name="Reem Ashraf", Salary=14000},
            new Employee() {Id=14, Name="Tamer Magdy", Salary=22000},
            new Employee() {Id=15, Name="Dina Wael", Salary=12500},
            new Employee() {Id=16, Name="Amr Gamal", Salary=19000},
            new Employee() {Id=17, Name="Salma Hesham", Salary=10500},
            new Employee() {Id=18, Name="Ziad Ehab", Salary=8000},
            new Employee() {Id=19, Name="Farida Sherif", Salary=21000},
            new Employee() {Id=20, Name="Hossam Reda", Salary=25000}
        };


        await db.AddRangeAsync(employees, ct);

        await db.SaveChangesAsync(ct);
    }
}
