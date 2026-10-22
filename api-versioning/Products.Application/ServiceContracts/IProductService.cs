using Products.Application.DTO;

namespace Products.Application.ServiceContracts;

public interface IProductService
{
    public Task<ProductDetailsDto?> GetByIdAsync(int id, CancellationToken ct);
}
