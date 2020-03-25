using FluentAssertions;
using FTStore.Domain.Entities;
using FTStore.Domain.Tests.Entities.Prototypes;
using Xunit;

namespace FTStore.Domain.Tests.Entities
{
    public class OrderItemEntityTest
    {
        [Fact]
        public void ShouldCreateOrderItem()
        {
            var product = ProductEntityPrototype.GetValidProduct();

            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.QUANTITY,
                OrderItemEntityPrototype.DISCOUNT);

            orderItem.Should().NotBeNull();
        }

        [Fact]
        public void ShouldCopyProductsProperty()
        {
            var product = ProductEntityPrototype.GetValidProduct();

            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.QUANTITY,
                OrderItemEntityPrototype.DISCOUNT);

            orderItem.ProductId.Should().Be(product.Id);
            orderItem.Title.Should().BeSameAs(product.Title);
            orderItem.Price.Should().Be(product.Price);
            orderItem.Product.Should().BeSameAs(product);
        }

        [Fact]
        public void ShouldBeValidWithoutDiscount()
        {
            var product = ProductEntityPrototype.GetValidProduct();
            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.QUANTITY,
                OrderItemEntityPrototype.NO_DISCOUNT);

            var isValid = orderItem.IsValid();

            isValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldCalcTotalItem()
        {
            var product = ProductEntityPrototype.GetValidProduct();
            var totalItemExpected =
                (product.Price * OrderItemEntityPrototype.QUANTITY)
                - OrderItemEntityPrototype.DISCOUNT;

            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.QUANTITY,
                OrderItemEntityPrototype.DISCOUNT);

            orderItem.Total.Should().Be(totalItemExpected);
        }

        [Fact]
        public void ShouldBeValidWhenTotalIsZero()
        {
            var product = ProductEntityPrototype.GetValidProduct();
            var discountToZero = (product.Price * OrderItemEntityPrototype.QUANTITY);
            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.QUANTITY, discountToZero);

            var isValid = orderItem.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeInvalidWhenProductIsInvalid()
        {
            var invalidProduct = ProductEntityPrototype.GetInvalidProduct();
            var orderItem = new OrderItemEntity(
                invalidProduct,
                OrderItemEntityPrototype.QUANTITY,
                OrderItemEntityPrototype.DISCOUNT);

            var isValid = orderItem.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeInvalidWhenQuantityIsEqualOrLessThanZero()
        {
            var product = ProductEntityPrototype.GetValidProduct();
            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.INVALID_QUANTITY,
                OrderItemEntityPrototype.DISCOUNT);
            var expectedMessage = $"The quantity of {product.Title} must be greather than zero";

            var isValid = orderItem.IsValid();
            var errors = orderItem.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == expectedMessage);
        }

        [Fact]
        public void ShouldBeInvalidWhenDiscountIsNegative()
        {
            var product = ProductEntityPrototype.GetValidProduct();
            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.QUANTITY,
                OrderItemEntityPrototype.INVALID_DISCOUNT);
            var expectedMessage = $"The discount to {product.Title} should not be negative";

            var isValid = orderItem.IsValid();
            var errors = orderItem.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == expectedMessage);
        }

        [Fact]
        public void ShouldBeInvalidWhenTotalIsNegative()
        {
            var borderDiscount = 0.01M;
            var product = ProductEntityPrototype.GetValidProduct();
            var discountToNegative = (product.Price * OrderItemEntityPrototype.QUANTITY) + borderDiscount;
            var orderItem = new OrderItemEntity(
                product,
                OrderItemEntityPrototype.QUANTITY,
                discountToNegative);
            var expectedMessage = $"The total of product \"{orderItem.Title}\" can not be negative";

            var isValid = orderItem.IsValid();
            var errors = orderItem.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == expectedMessage);
        }
    }
}
