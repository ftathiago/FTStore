using System;

using FluentAssertions;

using FTStore.Domain.Entities;
using FTStore.Domain.Enum;
using FTStore.Domain.ValueObjects;

using Xunit;

namespace FTStore.Domain.Tests.Entities
{
    public class OrderEntityTest
    {

        private readonly DateTime OrderDate;
        private readonly UserEntity User;
        private readonly DateTime DeliveryForecast;
        private readonly Address DeliveryAddress;
        private readonly PaymentMethod PaymentMethod;

        private const int USER_ID = 1;
        private const int PAYMENTMETHOD_UNKNOW = 0;

        public OrderEntityTest()
        {
            OrderDate = DateTime.Now;
            DeliveryForecast = OrderDate.AddDays(10);

            User = new UserEntity();
            User.DefineId(1);

            DeliveryAddress = new Address("street", addressNumber: 173, "neighborhood",
                "city", "state");
            PaymentMethod = new PaymentMethod(PaymentMethodEnum.CreditCard);
        }

        #region Creating Test

        [Fact]
        public void ShouldCreateOrder()
        {
            var order = new OrderEntity(OrderDate, User, DeliveryForecast, DeliveryAddress,
                PaymentMethod);

            order.Should().NotBeNull();
        }

        [Fact]
        public void CanCreateAOrderWithForeignKeys()
        {
            var order = OrderEntity.CreateWithForeignIds(OrderDate, USER_ID, DeliveryForecast,
                DeliveryAddress, PaymentMethodEnum.CreditCard);

            order.Should().NotBeNull();
        }

        #endregion

        #region Required Validations
        [Fact]
        public void ShouldBeInvalidWhenOrderDoesNotHaveAUser()
        {
            const string EXPECTED_ERROR_MESSAGE = "A user is required";
            var order = OrderEntity.CreateWithForeignIds(OrderDate, USER_ID, DeliveryForecast,
                DeliveryAddress, PaymentMethodEnum.CreditCard);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ShouldBeInvalidWhenDoesNotHaveOrderItems()
        {
            const string EXPECTED_ERROR_MESSAGE = "A Order must have one item at least";
            var order = new OrderEntity(OrderDate, User, DeliveryForecast,
                DeliveryAddress, PaymentMethod);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ShouldBeInvalidWhenOrderDoesNotHaveDeliveryAddress()
        {
            const string EXPECTED_ERROR_MESSAGE = "Delivery address is required";
            var order = new OrderEntity(OrderDate, User, DeliveryForecast,
                deliveryAddress: null, PaymentMethod);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ShouldBeInvalidWhenPaymentMethodIsUnknow()
        {
            const string EXPECTED_ERROR_MESSAGE = "A Payment method must be specified";
            var order = OrderEntity.CreateWithForeignIds(OrderDate, USER_ID,
                DeliveryForecast, DeliveryAddress, PaymentMethodEnum.Unknow);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        #endregion

        [Fact]
        public void ShouldBeInvalidWhenDeliveryForeCastBeforeOrderDate()
        {
            var invalidDeliveryForecast = OrderDate.AddDays(-1);
            var order = new OrderEntity(OrderDate, User, invalidDeliveryForecast,
                DeliveryAddress, PaymentMethod);

            var isValid = order.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldHaveEspecificErrorMessageWhenDeliveryForecastIsInvalid()
        {
            const string EXPECTED_ERROR_MESSAGE = "It is impossible to deliver before the ordering";
            var invalidDeliveryForecast = OrderDate.AddDays(-1);
            var order = new OrderEntity(OrderDate, User, invalidDeliveryForecast,
                DeliveryAddress, PaymentMethod);
            order.IsValid();

            var errors = order.ValidationResult.Errors;

            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ShouldBeSincronizeUserEntityAndUserId()
        {
            var order = new OrderEntity(OrderDate, User, DeliveryForecast,
                DeliveryAddress, PaymentMethod);

            order.UserId.Should().Be(User.Id);
        }

        #region OrderItem Control
        [Fact]
        public void ShouldCanAddOrderItem()
        {
            var order = new OrderEntity(OrderDate, User, DeliveryForecast,
                DeliveryAddress, PaymentMethod);
            var orderItem = new OrderItemEntity();

            order.AddItem(orderItem);

            order.OrderItems.Should().HaveCount(1);
        }
        #endregion
    }
}
