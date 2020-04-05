using FTStore.Domain.Repository;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;
using FTStore.Infra.Table;
using AutoMapper;
using FTStore.Infra.Common.Repository;

namespace FTStore.Infra.Repository
{
    public class ProductRepository : BaseRepository<Product, ProductTable>, IProductRepository
    {
        public ProductRepository(FTStoreDbContext FTStoreContexto, IMapper mapper)
            : base(FTStoreContexto, mapper) { }
    }
}
