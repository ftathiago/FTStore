using FluentValidation;
using FTStore.Domain.Entities;
using FTStore.Domain.Enum;
using System.Linq;

namespace FTStore.Domain.Validations
{
    public class OrderEntityValidations : AbstractValidator<OrderEntity>
    {
        private const string USER_REQUIRED = "A user is required";
        public OrderEntityValidations()
        {
            RuleFor(order => order.DeliveryForecast)
                .GreaterThanOrEqualTo(order => order.OrderDate)
                .WithMessage("It is impossible to deliver before the ordering");

            RuleFor(order => order.OrderItems)
                .Must(orderItems => orderItems != null && orderItems.Any())
                .WithMessage("A Order must have one item at least");

            RuleFor(order => order.User)
                .NotNull()
                .WithMessage(USER_REQUIRED);

            RuleFor(order => order.DeliveryAddress)
                .NotNull()
                .WithMessage("Delivery address is required");

            RuleFor(order => order.PaymentMethodId)
                .NotEqual((int)PaymentMethodEnum.Unknow)
                .WithMessage("A Payment method must be specified");
        }
    }
}
