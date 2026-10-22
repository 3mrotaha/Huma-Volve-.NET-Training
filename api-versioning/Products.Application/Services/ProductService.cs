using Prodcuts.Domain.AppContext;
using Prodcuts.Domain.Entities;
using Products.Application.DTO;
using Products.Application.ServiceContracts;

namespace Products.Application.Services;

public class ProductService(AppDbContext db) : IProductService
{
    private readonly AppDbContext _db = db;
 
    public async Task<ProductDetailsDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        // get the product from database
        Product? product = await _db.Products.FindAsync(id);

        if(product is null)
            return null;

        return product.ToProductDetails();
    }
}
