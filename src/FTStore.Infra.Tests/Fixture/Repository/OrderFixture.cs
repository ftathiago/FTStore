using System;
using FTStore.Domain.Entities;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Model;

namespace FTStore.App.Tests.Fixture.Repository
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

        public OrderModel GetValid(CustomerModel customer, ProductModel product, PaymentMethodModel paymentMethod)
        {
            var address = Address;
            var order = new OrderModel
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
                new OrderItemModel
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

        public OrderEntity GetValidEntity(ProductEntity product, CustomerEntity customer, int orderId = ID)
        {
            var orderItem = new OrderItemEntity(product, QUANTITY, DISCOUNT);
            orderItem.DefineId(ID);
            var order = new OrderEntity(
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
