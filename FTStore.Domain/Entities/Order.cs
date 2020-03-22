using System;
using System.Collections.Generic;
using System.Linq;
using FTStore.Domain.ValueObjects;

namespace FTStore.Domain.Entities
{
    public class OrderEntity : FTStore.Domain.Common.Entities.Entity
    {
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public DateTime DeliveryForecast { get; set; }

        public Address DeliveryAddress { get; private set; }
        public int FormaPagamentoId { get; set; }
        public virtual PaymentMethod FormaPagamento { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
