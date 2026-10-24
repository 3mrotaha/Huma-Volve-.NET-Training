using System.Data.Common;
using MediatR;
using Products.Application.Abstractions;

namespace Products.Application.Product.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler(IAppDbContext dbContext) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync(request.Id, cancellationToken);

        if(product is null)
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");

        product.Name = request.Name;
        product.Price = request.Price;
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}