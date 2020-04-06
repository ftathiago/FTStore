using Microsoft.EntityFrameworkCore;

using AutoMapper;

using FTStore.Domain.Entities;
using FTStore.Domain.Repositories;
using FTStore.Infra.Common.Repositories;
using FTStore.Infra.Context;
using FTStore.Infra.Tables;


namespace FTStore.Infra.Repositories
{
    public class OrderRepository : BaseRepository<Order, OrderTable>, IOrderRepository
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
