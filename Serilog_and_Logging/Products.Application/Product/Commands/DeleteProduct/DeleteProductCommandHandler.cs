using MediatR;
using Microsoft.Extensions.Logging;
using Products.Application.Abstractions;

namespace Products.Application.Product.Commands.DeleteProduct;


public class DeleteProductCommandHandler(IAppDbContext dbContext, ILogger<DeleteProductCommandHandler> logger) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync(request.Id, cancellationToken);

        if(product is null)
        {
            logger.LogWarning("Couldn't find product with Id {id}", request.Id);
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted product with Id {id} successfully", request.Id);
    }
}