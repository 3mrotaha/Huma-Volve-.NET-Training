using System.Data.Common;
using MediatR;
using Products.Application.Abstractions;

namespace Products.Application.Product.Commands.CreateProduct;

public sealed class CreateProductCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price
        };

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}