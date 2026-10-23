namespace Products.Application.DTO;

public record ProductDetailsDto(
    string Name,
    decimal PriceAmount,
    string Currency,
    string CategoryName
);