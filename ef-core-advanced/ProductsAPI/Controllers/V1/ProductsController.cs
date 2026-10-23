using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Products.Application.ServiceContracts;
using Products.Application;
namespace ProductsAPI.Controllers.V1;

[ApiVersion(1.0)]
[Route("api/v{vesrion:apiVersion}/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var product = await _productService.GetByIdAsync(id, ct);

        if(product is null)
            return NotFound();

        var response = product.ToProductResponseV1();
        
        return Ok(response);
    }
}

