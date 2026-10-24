using MediatR;

namespace Products.Application.Product.Commands.UpdateProduct;

public sealed record UpdateProductCommand(Guid Id, string Name, decimal Price) : IRequest;