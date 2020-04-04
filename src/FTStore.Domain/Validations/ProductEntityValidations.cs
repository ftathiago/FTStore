using FluentValidation;
using FTStore.Domain.Entities;

namespace FTStore.Domain.Validations
{
    public class ProductEntityValidations : AbstractValidator<Product>
    {

        public ProductEntityValidations()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("The product's title is required");

            RuleFor(product => product.Details)
                .MinimumLength(50)
                .WithMessage("The product's description must have 50 characters at least");

            RuleFor(product => product.Price)
                .GreaterThan(0)
                .WithMessage("The product's prices must be greater than zero");
        }
    }
}
