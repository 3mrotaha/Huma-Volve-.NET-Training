using System.Data.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Application.Abstractions;
using Products.Application.DTOs.Product;

namespace Products.Application.Product.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler(IAppDbContext dbContext, ILogger<GetProductByIdQueryHandler> logger) : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
{
    public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync(request.Id, cancellationToken);

        if(product is null)
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");

        logger.LogInformation("Successful Query Product with Id {Id}", product.Id);

        return new ProductResponseDto(product.Name, product.Price);
    }
}