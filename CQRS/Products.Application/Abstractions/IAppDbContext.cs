using Microsoft.EntityFrameworkCore;
using Products;

namespace Products.Application.Abstractions;

public interface IAppDbContext
{
    public DbSet<Domain.Entities.Product> Products {get;}

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
