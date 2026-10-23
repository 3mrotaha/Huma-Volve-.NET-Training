using Prodcuts.Domain.Entities;
using Products.Application.DTO;
using Products.Application.DTO.V1;

namespace Products.Application;

public static class ProductExtensions
{
    public static ProductDTO ToProductResponseV1(this ProductDetailsDto pd)
    {
        return new ProductDTO(pd.Name, pd.PriceAmount, pd.CategoryName);
    }

    public static DTO.V2.ProductDTO ToProductResponseV2(this ProductDetailsDto pd)
    {
        return new DTO.V2.ProductDTO(pd.Name, 
                    new DTO.V2.PriceResponse(pd.PriceAmount, pd.Currency), pd.CategoryName);
    } 
}
