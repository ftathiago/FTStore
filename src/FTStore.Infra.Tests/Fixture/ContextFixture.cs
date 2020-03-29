using System;
using System.Linq;
using FTStore.Infra.Context;
using FTStore.App.Tests.Fixture.Repository;
using Microsoft.EntityFrameworkCore;


namespace FTStore.Infra.Tests.Fixture
{
    public class ContextFixture : IDisposable
    {
        private bool _disposed;

        public FTStoreDbContext Ctx => FakeFTStoreDbContext();

        private static FTStoreDbContext FakeFTStoreDbContext()
        {
            var options = new DbContextOptionsBuilder<FTStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new FTStoreDbContext(options, new HostEnvironmentFixture());
        }

        public void InitializeWithOneProduct(FTStoreDbContext context)
        {
            var product = new ProductFixture().GetValid(ProductFixture.ID);
            context.Products.Add(product);
            context.SaveChanges();
            context.Entry(product).State = EntityState.Detached;
        }

        public void InitializeWithOneCustomer(FTStoreDbContext context)
        {
            var customer = new CustomerFixture().GetValid(CustomerFixture.ID);
            context.Customers.Add(customer);
            context.SaveChanges();
            context.Entry(customer).State = EntityState.Detached;
        }

        public void InitializeWithOnePaymentMethod(FTStoreDbContext context)
        {
            var paymentMethod = PaymentMethodFixture.GetValid();
            context.PaymentMethod.Add(paymentMethod);
            context.SaveChanges();
            context.Entry(paymentMethod).State = EntityState.Detached;
        }

        public void InitializeWithOneOrder(FTStoreDbContext context)
        {
            var customer = context.Customers.First();
            var product = context.Products.First();
            var paymentMethod = context.PaymentMethod.First();
            var order = new OrderFixture().GetValid(customer, product, paymentMethod);
            context.Orders.Add(order);
            context.SaveChanges();
            context.Entry(customer).State = EntityState.Detached;
            context.Entry(product).State = EntityState.Detached;
            context.Entry(paymentMethod).State = EntityState.Detached;
            context.Entry(order).State = EntityState.Detached;
        }

        ~ContextFixture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (_disposed) return;

            if (dispose)
            {
                Ctx?.Dispose();
            }

            _disposed = true;
        }
    }

}

