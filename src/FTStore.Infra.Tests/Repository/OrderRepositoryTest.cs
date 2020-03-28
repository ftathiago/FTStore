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
using FTStore.Infra.Tests.Prototype;
using Xunit;

namespace FTStore.Infra.Tests.Repository
{
    public class OrderRepositoryTest : BaseRepositoryTest<OrderModel>,
        IClassFixture<ProductPrototype>, IClassFixture<CustomerPrototype>,
        IClassFixture<OrderPrototype>
    {
        private const int ID = 1;
        private readonly ProductPrototype _productPrototype;
        private readonly CustomerPrototype _customerPrototype;
        private readonly OrderPrototype _orderPrototype;

        public OrderRepositoryTest(
            ContextFixture contextFixture,
            AutoMapperFixture autoMapperFixture,
            ProductPrototype productPrototype,
            CustomerPrototype customerPrototype,
            OrderPrototype orderPrototype)
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

            var orderEntity = repository.GetById(OrderPrototype.ID);

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
            var product = new ProductPrototype().GetValid();
            var customer = new CustomerPrototype().GetValid();
            var paymentMethod = PaymentMethodPrototype.GetValid();
            return new OrderPrototype().GetValid(customer, product, paymentMethod);
        }

        private void InitializeDataBase(FTStoreDbContext context)
        {
            ContextFixture.InitializeWithOneCustomer(context);
            ContextFixture.InitializeWithOneProduct(context);
            ContextFixture.InitializeWithOnePaymentMethod(context);
        }
    }
}
