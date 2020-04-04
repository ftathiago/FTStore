using System;
using System.Collections.Generic;
using System.Linq;
using FTStore.Domain.Common.Entities;
using FTStore.Domain.Validations;
using FTStore.Domain.ValueObjects;

namespace FTStore.Domain.Entities
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public DateTime DeliveryForecast { get; protected set; }

        public Address DeliveryAddress { get; protected set; }
        public virtual PaymentMethod PaymentMethod { get; protected set; }
        public virtual ICollection<OrderItem> OrderItems { get; protected set; }

        protected Order() : base()
        {
            OrderItems = new List<OrderItem>();
        }
        public Order(DateTime orderDate, Customer customer, DateTime deliveryForecast,
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

        public void AddItem(OrderItem orderItem)
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
