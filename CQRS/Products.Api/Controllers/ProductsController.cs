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

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetProductsQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await mediator.Send(new GetProductByIdQuery(id));
            
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto productRequest)
        {
            var result = await mediator.Send(new CreateProductCommand(productRequest.Name, productRequest.Price));

            return CreatedAtRoute("GetById", new {id = result}, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequestDto productRequest)
        {
            await mediator.Send(new UpdateProductCommand(id, productRequest.Name, productRequest.Price));

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }
    }
}
