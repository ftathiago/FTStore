using System.Collections.Generic;
using FTStore.Domain.Repository;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;

namespace FTStore.Infra.Repository
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(FTStoreDbContext FTStoreContexto) : base(FTStoreContexto)
        {
        }
    }
}
