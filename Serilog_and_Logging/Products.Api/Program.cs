using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Products.Api.Exceptions;
using Products.Application;
using Products.Application.Abstractions;
using Products.Application.Behaviors;
using Products.Infrastructure.Data;
using Products.Infrastructure.Data.Seeder;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "Products Api")
        .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
        .WriteTo.File("logs/log-.text", rollingInterval: RollingInterval.Day)
        .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddTransient<IAppDbContext, AppDbContext>();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(IAssemblyMarker).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
builder.Services.AddControllers();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using (var scope = app.Services.CreateAsyncScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // await DataSeeder.SeedDataAsync(dbContext);
    }
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
