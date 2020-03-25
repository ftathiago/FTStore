using System;
using System.Collections.Generic;
using System.Linq;
using FTStore.Domain.Enum;
using FTStore.Domain.Validations;
using FTStore.Domain.ValueObjects;

namespace FTStore.Domain.Entities
{
    public class OrderEntity : FTStore.Domain.Common.Entities.Entity
    {
        public DateTime OrderDate { get; protected set; }
        public int CustomerId { get; protected set; }
        public virtual CustomerEntity Customer { get; protected set; }
        public DateTime DeliveryForecast { get; protected set; }

        public Address DeliveryAddress { get; protected set; }
        public int PaymentMethodId { get; protected set; }
        public virtual PaymentMethod PaymentMethod { get; protected set; }
        public virtual ICollection<OrderItemEntity> OrderItems { get; protected set; }

        protected OrderEntity() : base()
        {
            OrderItems = new List<OrderItemEntity>();
        }
        public OrderEntity(DateTime orderDate, CustomerEntity customer, DateTime deliveryForecast,
            Address deliveryAddress, PaymentMethod paymentMethod)
            : this()
        {
            OrderDate = orderDate;
            Customer = customer;
            CustomerId = customer != null ? customer.Id : 0;

            DeliveryForecast = deliveryForecast;
            DeliveryAddress = deliveryAddress;
            PaymentMethodId = paymentMethod != null ? paymentMethod.Id : (int)PaymentMethodEnum.Unknow;
            PaymentMethod = paymentMethod;
        }

        public static OrderEntity CreateWithForeignIds(DateTime orderDate, int customerId, DateTime deliveryForecast,
            Address deliveryAddress, PaymentMethodEnum paymentMethod)
        {
            var orderEntity = new OrderEntity();
            orderEntity.OrderDate = orderDate;
            orderEntity.CustomerId = customerId;
            orderEntity.DeliveryForecast = deliveryForecast;
            orderEntity.DeliveryAddress = deliveryAddress;
            orderEntity.PaymentMethodId = (int)paymentMethod;
            return orderEntity;
        }

        public override bool IsValid()
        {
            _validationResult = new OrderEntityValidations().Validate(this);
            return _validationResult.IsValid;
        }

        public void AddItem(OrderItemEntity orderItem)
        {
            OrderItems.Add(orderItem);
        }

        public decimal Total()
        {
            return OrderItems.Sum(orderItem => orderItem.Total);
        }
    }
}
