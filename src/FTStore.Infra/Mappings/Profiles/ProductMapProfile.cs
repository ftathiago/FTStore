using AutoMapper;
using FTStore.Domain.Entities;
using FTStore.Infra.Table;

namespace FTStore.Infra.Mappings.Profiles
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<Product, ProductTable>();
            CreateMap<ProductTable, Product>();
        }

    }
}
