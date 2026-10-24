using MediatR;

namespace Products.Application.Product.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;