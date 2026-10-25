using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTOs.Product;
using Products.Application.Product.Commands.CreateProduct;
using Products.Application.Product.Commands.DeleteProduct;
using Products.Application.Product.Commands.UpdateProduct;
using Products.Application.Product.Queries.GetProductById;
using Products.Application.Product.Queries.GetProducts;
using Products.Domain.Entities;
using Serilog;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator, ILogger<ProductsController> logger) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetProductsQuery());

            logger.LogInformation("Successfully queried {count} products", result?.Count);
            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await mediator.Send(new GetProductByIdQuery(id));
            
            logger.LogInformation("Successfully queried product {product}", result);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto productRequest)
        {
            var result = await mediator.Send(new CreateProductCommand(productRequest.Name, productRequest.Price));

            logger.LogInformation("Successfully Created product {productId}", result);

            return CreatedAtRoute("GetById", new {id = result}, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequestDto productRequest)
        {
            await mediator.Send(new UpdateProductCommand(id, productRequest.Name, productRequest.Price));

            logger.LogInformation("Successfully Updated product {productId}", id);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteProductCommand(id));

            logger.LogInformation("Successfully Deleted product {productId}", id);

            return NoContent();
        }
    }
}
