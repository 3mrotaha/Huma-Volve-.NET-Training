using System.Data;
using FluentValidation;

namespace Products.Application.Product.Commands.CreateProduct;


public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product Name Can't be Empty");
        
        RuleFor(x => x.Price)
            .NotEqual(0)
            .WithMessage("Product Price should be > 0");
    }
}