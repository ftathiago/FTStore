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
        protected override OrderModel BeforePost(OrderModel model, EntityState state)
        {
            Unchange<CustomerModel>(model.Customer);
            Unchange<PaymentMethodModel>(model.PaymentMethod);

            return base.BeforePost(model, state);
        }

    }
}
