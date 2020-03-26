using System.Collections.Generic;
using FTStore.Domain.Repository;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;
using FTStore.Infra.Model;
using AutoMapper;

namespace FTStore.Infra.Repository
{
    public class ProductRepository : BaseRepository<ProductEntity, ProductModel>, IProductRepository
    {
        public ProductRepository(FTStoreDbContext FTStoreContexto, IMapper mapper)
            : base(FTStoreContexto, mapper) { }
    }
}
