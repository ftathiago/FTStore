using AutoMapper;

using FTStore.Domain.Entities;
using FTStore.Domain.Repositories;

using FTStore.Lib.Common.Repositories;
using FTStore.Infra.Context;
using FTStore.Infra.Tables;


namespace FTStore.Infra.Repositories
{
    public class ProductRepository : BaseRepository<Product, ProductTable>, IProductRepository
    {
        public ProductRepository(FTStoreDbContext FTStoreContexto, IMapper mapper)
            : base(FTStoreContexto, mapper) { }
    }
}
