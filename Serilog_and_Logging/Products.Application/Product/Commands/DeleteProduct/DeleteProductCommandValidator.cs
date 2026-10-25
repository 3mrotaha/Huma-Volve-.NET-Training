using FluentValidation;
using MediatR;

namespace Products.Application.Product.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Can't Use Null as Product Id");
    }
}