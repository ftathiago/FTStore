using System;
using System.Collections.Generic;

namespace FTStore.Infra.Table
{
    public class OrderTable
    {
        public OrderTable()
        {
            OrderItems = new List<OrderItemTable>();
        }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public virtual CustomerTable Customer { get; set; }
        public DateTime DeliveryForecast { get; set; }

        public string Street { get; set; }
        public string AddressNumber { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethodTable PaymentMethod { get; set; }
        public virtual ICollection<OrderItemTable> OrderItems { get; protected set; }
    }
}
