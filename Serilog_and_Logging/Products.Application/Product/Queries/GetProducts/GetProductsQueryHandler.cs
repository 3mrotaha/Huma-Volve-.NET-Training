using System.Data.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Application.Abstractions;
using Products.Application.DTOs.Product;
using Products.Application.Product.Queries.GetProductById;
using Products.Application.Product.Queries.GetProducts;

namespace Products.Application.Product.Queries.GetProducts;

public sealed class GetProductsQueryHandler(IAppDbContext dbContext, ILogger<GetProductsQueryHandler> logger) : IRequestHandler<GetProductsQuery, List<ProductResponseDto>?>
{
    public async Task<List<ProductResponseDto>?> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
                            .Select(p => new ProductResponseDto(p.Name, p.Price))
                            .ToListAsync(cancellationToken);

        logger.LogInformation("Successful Query {count} Products", products.Count);
        
        return products;
    }
}