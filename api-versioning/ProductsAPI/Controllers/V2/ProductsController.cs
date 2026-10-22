using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Application;
using Products.Application.ServiceContracts;

namespace ProductsAPI.Controllers.V2
{
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);

            if (product is null)
                return NotFound();

            var response = product.ToProductResponseV2();
            
            return Ok(response);
        }
    }
}
