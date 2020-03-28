using System.Collections.Generic;
using FTStore.Domain.Repository;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;
using FTStore.Infra.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace FTStore.Infra.Repository
{
    public class OrderRepository : BaseRepository<OrderEntity, OrderModel>, IOrderRepository
    {
        public OrderRepository(FTStoreDbContext FTStoreContexto, IMapper mapper)
            : base(FTStoreContexto, mapper)
        { }
        protected override OrderModel BeforePost(OrderModel order, EntityState state)
        {
            Unchange<CustomerModel>(order.Customer);
            Unchange<PaymentMethodModel>(order.PaymentMethod);

            return base.BeforePost(order, state);
        }

    }
}
