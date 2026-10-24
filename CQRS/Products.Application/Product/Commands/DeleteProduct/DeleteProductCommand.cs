using MediatR;

namespace Products.Application.Product.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest;