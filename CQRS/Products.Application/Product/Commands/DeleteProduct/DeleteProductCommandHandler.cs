using MediatR;
using Products.Application.Abstractions;

namespace Products.Application.Product.Commands.DeleteProduct;


public class DeleteProductCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync(request.Id, cancellationToken);

        if(product is null)
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}