using FTStore.Domain.Repository;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;
using FTStore.Infra.Table;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace FTStore.Infra.Repository
{
    public class OrderRepository : BaseRepository<OrderEntity, OrderTable>, IOrderRepository
    {
        public OrderRepository(FTStoreDbContext FTStoreContexto, IMapper mapper)
            : base(FTStoreContexto, mapper)
        { }
        protected override OrderTable BeforePost(OrderTable model, EntityState state)
        {
            Unchange<CustomerTable>(model.Customer);
            Unchange<PaymentMethodTable>(model.PaymentMethod);

            return base.BeforePost(model, state);
        }

    }
}
