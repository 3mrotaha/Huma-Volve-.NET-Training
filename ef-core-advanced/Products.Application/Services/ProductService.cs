using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Prodcuts.Domain.AppContext;
using Prodcuts.Domain.Entities;
using Products.Application.DTO;
using Products.Application.DTO.V2;
using Products.Application.ServiceContracts;

namespace Products.Application.Services;

public class ProductService(AppDbContext db) : IProductService
{
    private readonly AppDbContext _db = db;
 
    public async Task<ProductDetailsDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        // get the product from database
        var product = await _db.Products.AsNoTracking()
                                            .Include(p => p.Category)
                                            .Where(p => p.Id == id)
                                            .Select(p => new ProductDetailsDto(p.Name, p.Price, "EGP", p.CategoryId == null? string.Empty : p.Category!.Name))
                                            .FirstOrDefaultAsync(ct);

        if(product is null)
            return null;

        return product;
    }

    public async Task<List<ProductDetailsDto>?> GetProductsAsync(int page, int pageSize, CancellationToken ct)
    {
        var products = await _db.Products.AsNoTracking()
                                .Include(p => p.Category)
                                .Select(p => new ProductDetailsDto(p.Name, p.Price, "EGP", p.CategoryId == null? string.Empty : p.Category!.Name))
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync(ct);

        if(products is null)
            return null;


        return products;
    }

    public async Task<List<ProductDetailsDto>?> GetProductsAsync(string categoryName, int page, int pageSize, CancellationToken ct)
    {
        var products = await _db.Products.AsNoTracking()
                        .Include(p => p.Category)
                        .Where(p => p.CategoryId != null &&  p.Category!.Name == categoryName)
                        .Select(p => new ProductDetailsDto(p.Name, p.Price, "EGP", p.Category!.Name))
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(ct);

        if(products is null)
            return null;


        return products;
    }

    public async Task<ProductUpdateResponse?> UpdateProduct(Guid id, ProductUpdateRequest request, CancellationToken ct)
    {
        try
        {
            var product = await _db.Products.Where(p => p.Id == id)
                                    .FirstOrDefaultAsync(ct);
            
            if(product is null)
                return null;

            product.SetName(request.Name);
            product.SetPrice(request.Price);

            await _db.SaveChangesAsync(ct);

            return new ProductUpdateResponse(product.Name, product.Price);
        }
        catch(DbUpdateConcurrencyException)
        {
            return null;
        }
    }
}
