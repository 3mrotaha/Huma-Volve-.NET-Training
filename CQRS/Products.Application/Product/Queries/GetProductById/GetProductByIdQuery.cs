using MediatR;
using Products.Application.DTOs.Product;

namespace Products.Application.Product.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductResponseDto>;