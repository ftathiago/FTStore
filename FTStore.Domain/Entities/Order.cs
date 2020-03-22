using System;
using System.Collections.Generic;
using System.Linq;
using FTStore.Domain.ValueObjects;

namespace FTStore.Domain.Entity
{
    public class Order : Entity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public DateTime DeliveryForecast { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string AddressNumber { get; set; }
        public int FormaPagamentoId { get; set; }
        public virtual PaymentMethod FormaPagamento { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public override void Validate()
        {
            if (!OrderItems.Any())
            {
                AdicionarCritica("There's no itens selected");
            }
            if (string.IsNullOrEmpty(ZipCode))
            {
                AdicionarCritica("Zipcode is required");
            }
        }
    }
}
