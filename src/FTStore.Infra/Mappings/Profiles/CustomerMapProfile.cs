using AutoMapper;
using FTStore.Domain.Entities;
using FTStore.Infra.Tables;

namespace FTStore.Infra.Mappings.Profiles
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<Customer, CustomerTable>();
            CreateMap<CustomerTable, Customer>();
        }
    }
}
