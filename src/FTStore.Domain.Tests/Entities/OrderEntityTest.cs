using System;

using FluentAssertions;

using FTStore.Domain.Entities;
using FTStore.Domain.Enum;
using FTStore.Domain.Tests.Fixture;
using FTStore.Domain.ValueObjects;

using Xunit;

namespace FTStore.Domain.Tests.Entities
{
    public class OrderEntityTest
    {

        private readonly DateTime OrderDate;
        private readonly CustomerEntity Customer;
        private readonly DateTime DeliveryForecast;
        private readonly Address DeliveryAddress;
        private readonly PaymentMethod PaymentMethod;
        private readonly PaymentMethod InvalidPaymentMethod;

        public OrderEntityTest()
        {
            OrderDate = DateTime.Now;
            DeliveryForecast = OrderDate.AddDays(10);

            Customer = new CustomerEntity("Name", "Surname");
            Customer.DefineId(1);

            DeliveryAddress = new Address(
                "street",
                addressNumber: "173",
                "neighborhood",
                "city",
                "state",
                zipCode: "00000000");
            PaymentMethod = new PaymentMethod(PaymentMethodEnum.CreditCard);
            InvalidPaymentMethod = new PaymentMethod(PaymentMethodEnum.Unknow);
        }

        #region Creating Test

        [Fact]
        public void ShouldCreateOrder()
        {
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast, DeliveryAddress,
                PaymentMethod);

            order.Should().NotBeNull();
        }

        #endregion

        #region Required Validations

        [Fact]
        public void ShouldBeInvalidWhenDoesNotHaveOrderItems()
        {
            const string EXPECTED_ERROR_MESSAGE = "A Order must have one item at least";
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast,
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
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast,
                deliveryAddress: null, PaymentMethod);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ShouldBeInvalidWhenPaymentMethodIsNull()
        {
            const string EXPECTED_ERROR_MESSAGE = "A Payment method must be specified";
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast,
                DeliveryAddress, null);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ShouldBeInvalidWhenCustomerIsNull()
        {
            const string EXPECTED_ERROR_MESSAGE = "Customer is required";
            var order = new OrderEntity(OrderDate, customer: null, DeliveryForecast,
                DeliveryAddress, PaymentMethod);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        #endregion

        #region OrderItem Control
        [Fact]
        public void ShouldCanAddOrderItem()
        {
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast,
                DeliveryAddress, PaymentMethod);
            var orderItem = OrderItemEntityFixture.GetValidOrderItem();

            order.AddItem(orderItem);

            order.OrderItems.Should().HaveCount(1);
        }

        [Fact]
        public void ShouldBeInvalidWhenOrderItemIsInvalid()
        {
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast,
                DeliveryAddress, PaymentMethod);
            var orderItem = OrderItemEntityFixture.GetInvalidOrderItem();
            order.AddItem(orderItem);

            var isValid = order.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldCalculateTotalItems()
        {
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast,
                DeliveryAddress, PaymentMethod);
            var orderItem1 = OrderItemEntityFixture.GetValidOrderItem(1);
            var orderItem2 = OrderItemEntityFixture.GetValidOrderItem(2);
            var expectedTotal = orderItem1.Total + orderItem2.Total;
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var total = order.Total();

            total.Should().Be(expectedTotal);
        }

        #endregion

        #region Guarantee invariants

        [Fact]
        public void ShouldBeInvalidWhenDeliveryForeCastBeforeOrderDate()
        {
            const string EXPECTED_ERROR_MESSAGE = "It is impossible to deliver before the ordering";
            var invalidDeliveryForecast = OrderDate.AddDays(-1);
            var order = new OrderEntity(OrderDate, Customer, invalidDeliveryForecast,
                DeliveryAddress, PaymentMethod);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ShouldBeInvalidWhenPaymentMethodIsUnknow()
        {
            const string EXPECTED_ERROR_MESSAGE = "A Payment method must be specified";
            var order = new OrderEntity(OrderDate, Customer, DeliveryForecast,
                DeliveryAddress, InvalidPaymentMethod);

            var isValid = order.IsValid();
            var errors = order.ValidationResult.Errors;

            isValid.Should().BeFalse();
            errors.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        #endregion

    }
}
