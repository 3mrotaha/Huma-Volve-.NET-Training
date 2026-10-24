using MediatR;
using Products.Application.DTOs.Product;

namespace Products.Application.Product.Queries.GetProducts;

public sealed record GetProductsQuery : IRequest<List<ProductResponseDto>?>;