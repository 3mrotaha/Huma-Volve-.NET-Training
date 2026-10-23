using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Products.Application;
using Products.Application.DTO.V2;
using Products.Application.ServiceContracts;
using Products.Application.Services;

namespace ProductsAPI.Controllers.V2
{
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);

            if (product is null)
                return NotFound();

            var response = product.ToProductResponseV2();
            
            return Ok(response);
        }

        [HttpGet("{page:int}/{size:int}")]
        public async Task<IActionResult> GetProductsAsync(int page, int size, CancellationToken ct)
        {
            var products = await _productService.GetProductsAsync(page, size, ct);

            if (products is null)
                return NotFound();

            var response = products.Select(p => p.ToProductResponseV2());
            
            return Ok(response);
        }

        [HttpGet("{category}/{page:int}/{size:int}")]
        public async Task<IActionResult> GetProductsAsync(string category, int page, int size, CancellationToken ct)
        {
            var products = await _productService.GetProductsAsync(category, page, size, ct);

            if (products is null)
                return NotFound();

            var response = products.Select(p => p.ToProductResponseV2());
            
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] ProductUpdateRequest request, CancellationToken ct)
        {
            var updatedProduct = await _productService.UpdateProduct(id, request, ct);

            if(updatedProduct == null)
                return BadRequest("product couldn't update, or not found");

            return Ok(updatedProduct);
        }
    }
}
