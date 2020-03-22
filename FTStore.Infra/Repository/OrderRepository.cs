using System.Collections.Generic;
using FTStore.Domain.Repository;
using FTStore.Domain.Entity;
using FTStore.Infra.Context;

namespace FTStore.Infra.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(FTStoreDbContext FTStoreContexto) : base(FTStoreContexto)
        {
        }
    }
}