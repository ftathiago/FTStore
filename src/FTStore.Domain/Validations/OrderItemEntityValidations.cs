using FluentValidation;
using FTStore.Domain.Entities;

namespace FTStore.Domain.Validations
{
    public class OrderItemEntityValidations : AbstractValidator<OrderItemEntity>
    {
        public OrderItemEntityValidations()
        {
            RuleFor(orderItem => orderItem.Product)
                .SetValidator(new ProductEntityValidations());
            RuleFor(orderItem => orderItem.Total)
                .GreaterThan(0)
                .WithMessage(orderItem => $"The total of product \"{orderItem.Title}\" can not be negative");
            RuleFor(orderItem => orderItem.Quantity)
                .GreaterThan(0)
                .WithMessage(orderItem => $"The quantity of {orderItem.Title} must be greather than zero");
            RuleFor(orderItem => orderItem.Discount)
                .GreaterThanOrEqualTo(0)
                .WithMessage(orderItem => $"The discount to {orderItem.Title} should not be negative");
        }
    }
}
