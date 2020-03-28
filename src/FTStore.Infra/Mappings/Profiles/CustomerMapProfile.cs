using AutoMapper;
using FTStore.Domain.Entities;
using FTStore.Infra.Model;

namespace FTStore.Infra.Mappings.Profiles
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<CustomerEntity, CustomerModel>();
            CreateMap<CustomerModel, CustomerEntity>();
        }
    }
}
