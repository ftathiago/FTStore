using System;
using System.Collections.Generic;

namespace FTStore.Infra.Model
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual CustomerModel Customer { get; set; }
        public DateTime DeliveryForecast { get; set; }

        public string Street { get; set; }
        public string AddressNumber { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethodModel PaymentMethod { get; set; }
        public virtual ICollection<OrderItemModel> OrderItems { get; protected set; }
    }
}