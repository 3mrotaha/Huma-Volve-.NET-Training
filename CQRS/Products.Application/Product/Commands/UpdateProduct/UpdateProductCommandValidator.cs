using System.Data;
using FluentValidation;

namespace Products.Application.Product.Commands.UpdateProduct;


public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Can't Use Null as Product Id");
            
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product Name Can't be Empty");
        
        RuleFor(x => x.Price)
            .NotEqual(0)
            .WithMessage("Product Price should be > 0");
    }
}