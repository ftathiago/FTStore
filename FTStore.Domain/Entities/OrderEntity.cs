using System;
using System.Collections.Generic;
using System.Linq;
using FTStore.Domain.Common.Entities;
using FTStore.Domain.Enum;
using FTStore.Domain.Validations;
using FTStore.Domain.ValueObjects;

namespace FTStore.Domain.Entities
{
    public class OrderEntity : Entity
    {
        public DateTime OrderDate { get; protected set; }
        public virtual CustomerEntity Customer { get; protected set; }
        public DateTime DeliveryForecast { get; protected set; }

        public Address DeliveryAddress { get; protected set; }
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
            DeliveryForecast = deliveryForecast;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
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

        public void DefineNewDeliveryForecast(DateTime newDeliveryForecast)
        {
            DeliveryForecast = newDeliveryForecast;
        }

        public decimal Total()
        {
            return OrderItems.Sum(orderItem => orderItem.Total);
        }
    }
}
