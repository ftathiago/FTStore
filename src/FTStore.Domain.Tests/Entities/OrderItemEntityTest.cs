using FluentAssertions;
using FTStore.Domain.Entities;
using FTStore.Domain.Tests.Fixture;
using Xunit;

namespace FTStore.Domain.Tests.Entities
{
    public class OrderItemEntityTest
    {
        [Fact]
        public void ShouldCreateOrderItem()
        {
            var product = ProductEntityFixture.GetValidProduct();

            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.QUANTITY,
                OrderItemEntityFixture.DISCOUNT);

            orderItem.Should().NotBeNull();
        }

        [Fact]
        public void ShouldCopyProductsProperty()
        {
            var product = ProductEntityFixture.GetValidProduct();

            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.QUANTITY,
                OrderItemEntityFixture.DISCOUNT);

            orderItem.ProductId.Should().Be(product.Id);
            orderItem.Title.Should().BeSameAs(product.Name);
            orderItem.Price.Should().Be(product.Price);
            orderItem.Product.Should().BeSameAs(product);
        }

        [Fact]
        public void ShouldBeValidWithoutDiscount()
        {
            var product = ProductEntityFixture.GetValidProduct();
            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.QUANTITY,
                OrderItemEntityFixture.NO_DISCOUNT);

            var isValid = orderItem.IsValid();

            isValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldCalcTotalItem()
        {
            var product = ProductEntityFixture.GetValidProduct();
            var totalItemExpected =
                (product.Price * OrderItemEntityFixture.QUANTITY)
                - OrderItemEntityFixture.DISCOUNT;

            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.QUANTITY,
                OrderItemEntityFixture.DISCOUNT);

            orderItem.Total.Should().Be(totalItemExpected);
        }

        [Fact]
        public void ShouldBeValidWhenTotalIsZero()
        {
            var product = ProductEntityFixture.GetValidProduct();
            var discountToZero = (product.Price * OrderItemEntityFixture.QUANTITY);
            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.QUANTITY, discountToZero);

            var isValid = orderItem.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeInvalidWhenProductIsInvalid()
        {
            var invalidProduct = ProductEntityFixture.GetInvalidProduct();
            var orderItem = new OrderItem(
                invalidProduct,
                OrderItemEntityFixture.QUANTITY,
                OrderItemEntityFixture.DISCOUNT);

            var isValid = orderItem.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeInvalidWhenQuantityIsEqualOrLessThanZero()
        {
            var product = ProductEntityFixture.GetValidProduct();
            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.INVALID_QUANTITY,
                OrderItemEntityFixture.DISCOUNT);
            var expectedMessage = $"The quantity of {product.Name} must be greather than zero";

            var isValid = orderItem.IsValid();
            var errors = orderItem.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == expectedMessage);
        }

        [Fact]
        public void ShouldBeInvalidWhenDiscountIsNegative()
        {
            var product = ProductEntityFixture.GetValidProduct();
            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.QUANTITY,
                OrderItemEntityFixture.INVALID_DISCOUNT);
            var expectedMessage = $"The discount to {product.Name} should not be negative";

            var isValid = orderItem.IsValid();
            var errors = orderItem.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == expectedMessage);
        }

        [Fact]
        public void ShouldBeInvalidWhenTotalIsNegative()
        {
            var borderDiscount = 0.01M;
            var product = ProductEntityFixture.GetValidProduct();
            var discountToNegative = (product.Price * OrderItemEntityFixture.QUANTITY) + borderDiscount;
            var orderItem = new OrderItem(
                product,
                OrderItemEntityFixture.QUANTITY,
                discountToNegative);
            var expectedMessage = $"The total of product \"{orderItem.Title}\" can not be negative";

            var isValid = orderItem.IsValid();
            var errors = orderItem.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == expectedMessage);
        }
    }
}
