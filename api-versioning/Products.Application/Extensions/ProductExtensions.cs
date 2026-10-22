using Prodcuts.Domain.Entities;
using Products.Application.DTO;
using Products.Application.DTO.V1;

namespace Products.Application;

public static class ProductExtensions
{
    public static ProductDetailsDto ToProductDetails(this Product p)
    {
        return new ProductDetailsDto(p.Name, p.Price, "EGP");
    }
    public static ProductDTO ToProductResponseV1(this ProductDetailsDto pd)
    {
        return new ProductDTO(pd.Name, pd.PriceAmount);
    }

    public static DTO.V2.ProductDTO ToProductResponseV2(this ProductDetailsDto pd)
    {
        return new DTO.V2.ProductDTO(pd.Name, 
                    new DTO.V2.PriceResponse(pd.PriceAmount, pd.Currency));
    } 
}
