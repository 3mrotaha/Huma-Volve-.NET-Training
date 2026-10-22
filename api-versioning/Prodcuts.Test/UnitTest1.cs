using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Prodcuts.Test;

public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Test_Api_V1_Price_Type_ReturnTrue()
    {
        var response = await _client.GetAsync("/api/v1/products/10");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using(var document = JsonDocument.Parse(json))
        {
            var price = document.RootElement.GetProperty("productPrice");
            
            Assert.Equal(JsonValueKind.Number, price.ValueKind);
        }
    }

    [Fact]
    public async Task Test_Api_V2_Price_Type_ReturnTrue()
    {
        var response = await _client.GetAsync("/api/v2/products/10");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using(var document = JsonDocument.Parse(json))
        {
            var price = document.RootElement.GetProperty("productPrice");
            
            Assert.Equal(JsonValueKind.Object, price.ValueKind);
        }
    }
}
