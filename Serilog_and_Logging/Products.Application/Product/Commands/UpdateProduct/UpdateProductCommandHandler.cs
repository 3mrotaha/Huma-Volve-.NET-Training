using System.Data.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Application.Abstractions;

namespace Products.Application.Product.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler(IAppDbContext dbContext, ILogger<UpdateProductCommandHandler> logger) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync(request.Id, cancellationToken);

        if(product is null)
        {
            logger.LogWarning("Couldn't find product with id {id}", request.Id);
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");
        }

        product.Name = request.Name;
        product.Price = request.Price;
        
        logger.LogInformation("updated product with Id {id} successfully", request.Id);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}