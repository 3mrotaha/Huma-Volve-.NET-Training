using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Employees.Domain.AppContext;
using Employees.Application.ServiceContracts;
using Employees.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("MyApp");
});
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);

    options.AssumeDefaultVersionWhenUnspecified = false;

    options.ReportApiVersions = true;

    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddMvc();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using(var scope = app.Services.CreateAsyncScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await DataSeeder.SeedAsync(db, app.Lifetime.ApplicationStopping);
    }
}

app.MapControllers();
// app.UseHttpsRedirection();
app.UseRouting();
app.Run();

public partial class Program {}
