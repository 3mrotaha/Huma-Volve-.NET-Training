using System.Text.Json;

namespace Prodcuts.Test;

public class IntegrationTestProducts(HttpClient client)
{
    private readonly HttpClient _client = client;

    [Fact]
    public async Task Test_Api_V1_Price_Type()
    {
        var response = await _client.GetAsync("/api/v1/products/10");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using(var document = JsonDocument.Parse(json))
        {
            var price = document.RootElement.GetProperty("price");
            
            Assert.Equal(JsonValueKind.Number, price.ValueKind);
        }
    }

    [Fact]
    public async Task Test_Api_V2_Price_Type()
    {
        var response = await _client.GetAsync("/api/v2/products/10");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using(var document = JsonDocument.Parse(json))
        {
            var price = document.RootElement.GetProperty("price");
            
            Assert.Equal(JsonValueKind.Number, price.ValueKind);
        }
    }
}
