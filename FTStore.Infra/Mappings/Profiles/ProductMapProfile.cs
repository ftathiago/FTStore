using AutoMapper;
using FTStore.Domain.Entities;
using FTStore.Infra.Model;

namespace FTStore.Infra.Mappings.Profiles
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductEntity, ProductModel>();
            CreateMap<ProductModel, ProductEntity>();
        }

    }
}
