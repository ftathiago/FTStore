using System;
using System.Linq;
using FluentAssertions;
using FTStore.Domain.Entities;
using FTStore.Domain.Enum;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Context;
using FTStore.Infra.Model;
using FTStore.Infra.Repository;
using FTStore.Infra.Tests.Fixture;
using FTStore.App.Tests.Fixture.Repository;
using Xunit;

namespace FTStore.Infra.Tests.Repository
{
    public class OrderRepositoryTest : BaseRepositoryTest<OrderModel>,
        IClassFixture<ProductFixture>, IClassFixture<CustomerFixture>,
        IClassFixture<OrderFixture>
    {
        private const int ID = 1;
        private readonly ProductFixture _productPrototype;
        private readonly CustomerFixture _customerPrototype;
        private readonly OrderFixture _orderPrototype;

        public OrderRepositoryTest(
            ContextFixture contextFixture,
            AutoMapperFixture autoMapperFixture,
            ProductFixture productPrototype,
            CustomerFixture customerPrototype,
            OrderFixture orderPrototype)
            : base(contextFixture, autoMapperFixture)
        {
            _productPrototype = productPrototype;
            _customerPrototype = customerPrototype;
            _orderPrototype = orderPrototype;
        }

        [Fact]
        public override void ShouldRegister()
        {
            using var context = ContextFixture.Ctx;
            InitializeDataBase(context);
            var customer = _customerPrototype.GetValidEntity();
            var product = _productPrototype.GetValidEntity();
            var order = _orderPrototype.GetValidEntity(product, customer);
            var repository = new OrderRepository(context, MapperFixture.Mapper);

            repository.Register(order);

            context.Orders.Should().ContainSingle();
        }

        [Fact]
        public override void ShouldConserveDataAfterRegister()
        {
            using var context = ContextFixture.Ctx;
            InitializeDataBase(context);
            var customer = _customerPrototype.GetValidEntity();
            var product = _productPrototype.GetValidEntity();
            var order = _orderPrototype.GetValidEntity(product, customer);
            var repository = new OrderRepository(context, MapperFixture.Mapper);
            repository.Register(order);

            var orderEntity = repository.GetById(OrderFixture.ID);

            orderEntity.OrderDate.Should().Be(order.OrderDate);
            orderEntity.Customer.Should().BeEquivalentTo(order.Customer);
            orderEntity.DeliveryForecast.Should().Be(order.DeliveryForecast);
            orderEntity.DeliveryAddress.Should().BeEquivalentTo(order.DeliveryAddress);
            orderEntity.PaymentMethod.Should().BeEquivalentTo(order.PaymentMethod);
            order.OrderItems.ToList().ForEach(orderItem =>
                orderEntity.OrderItems.Should().ContainSingle(oi =>
                    oi.Id == orderItem.Id &&
                    orderItem.Price == orderItem.Price &&
                    orderItem.ProductId == orderItem.ProductId &&
                    orderItem.Quantity == orderItem.Quantity &&
                    orderItem.Title == orderItem.Title &&
                    orderItem.Total == orderItem.Total &&
                    orderItem.Discount == orderItem.Discount
                ));
        }

        [Fact]
        public override void ShouldUpdate()
        {
            using var context = ContextFixture.Ctx;
            var repository = new OrderRepository(context, MapperFixture.Mapper);
            InitializeDataBase(context);
            ContextFixture.InitializeWithOneOrder(context);
            var order = repository.GetById(ID);
            var newDeliveryForecast = order.DeliveryForecast.AddDays(10);
            order.DefineNewDeliveryForecast(newDeliveryForecast);

            repository.Update(order);
            var modifiedOrder = repository.GetById(ID);

            modifiedOrder.DeliveryForecast.Should().Be(newDeliveryForecast);
        }

        [Fact]
        public override void ShouldDeleteByEntityReference()
        {
            using var context = ContextFixture.Ctx;
            InitializeDataBase(context);
            ContextFixture.InitializeWithOneOrder(context);
            OrderEntity orderEntity = new OrderEntity(DateTime.Now, null, DateTime.Now, null, null);
            orderEntity.DefineId(ID);
            var repository = new OrderRepository(context, MapperFixture.Mapper);

            repository.Remove(orderEntity);

            context.Orders.Should().BeEmpty();
        }

        protected override OrderModel GetModelPrototype(int id = 0)
        {
            var product = new ProductFixture().GetValid();
            var customer = new CustomerFixture().GetValid();
            var paymentMethod = PaymentMethodFixture.GetValid();
            return new OrderFixture().GetValid(customer, product, paymentMethod);
        }

        private void InitializeDataBase(FTStoreDbContext context)
        {
            ContextFixture.InitializeWithOneCustomer(context);
            ContextFixture.InitializeWithOneProduct(context);
            ContextFixture.InitializeWithOnePaymentMethod(context);
        }
    }
}
