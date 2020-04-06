using System;
using FTStore.Domain.Entities;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Tables;

namespace FTStore.Infra.Tests.Fixtures.Repositories
{
    public class OrderFixture
    {
        public const int ID = 1;
        public const decimal QUANTITY = 2M;
        public const decimal DISCOUNT = 0M;
        public readonly DateTime OrderDate;
        public readonly DateTime DeliveryForecast;
        public Address Address
        {
            get => new Address("street", "123", "neighborhood", "city", "state", "00000000");
        }
        public OrderFixture()
        {
            OrderDate = DateTime.Now;
            DeliveryForecast = OrderDate.AddDays(10);
        }

        public OrderTable GetValid(CustomerTable customer, ProductTable product, PaymentMethodTable paymentMethod)
        {
            var address = Address;
            var order = new OrderTable
            {
                Id = ID,
                OrderDate = this.OrderDate,
                CustomerId = customer.Id,
                Customer = customer,
                DeliveryForecast = this.DeliveryForecast,
                Street = address.Street,
                AddressNumber = address.AddressNumber,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State,
                ZIPCode = address.ZIPCode,
                PaymentMethodId = paymentMethod.Id,
                PaymentMethod = paymentMethod,
            };
            order.OrderItems.Add(
                new OrderItemTable
                {
                    Id = ID,
                    ProductId = product.Id,
                    Title = product.Name,
                    Price = product.Price,
                    Discount = DISCOUNT,
                    Quantity = QUANTITY
                });
            return order;
        }

        public Order GetValidEntity(Product product, Customer customer, int orderId = ID)
        {
            var orderItem = new OrderItem(product, QUANTITY, DISCOUNT);
            orderItem.DefineId(ID);
            var order = new Order(
                OrderDate,
                customer,
                DeliveryForecast,
                Address,
                PaymentMethodFixture.GetValidVO());
            order.DefineId(ID);
            order.AddItem(orderItem);

            return order;
        }




    }
}
