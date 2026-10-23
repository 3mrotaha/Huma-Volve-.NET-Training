using Prodcuts.Domain.Entities;
using Products.Application.DTO;
using Products.Application.DTO.V2;

namespace Products.Application.ServiceContracts;

public interface IProductService
{
    public Task<ProductDetailsDto?> GetByIdAsync(Guid id, CancellationToken ct);
    public Task<List<ProductDetailsDto>?> GetProductsAsync(int page, int pageSize, CancellationToken ct);
    public Task<List<ProductDetailsDto>?> GetProductsAsync(string categoryName, int page, int pageSize, CancellationToken ct);
    public Task<ProductUpdateResponse?> UpdateProduct(Guid id, ProductUpdateRequest request, CancellationToken ct);
}
