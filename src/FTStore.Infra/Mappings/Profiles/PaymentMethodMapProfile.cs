using AutoMapper;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Table;

namespace FTStore.Infra.Mappings.Profiles
{
    public class PaymentMethodMapProfile : Profile
    {
        public PaymentMethodMapProfile()
        {
            CreateMap<PaymentMethod, PaymentMethodTable>();
            CreateMap<PaymentMethodTable, PaymentMethod>();
        }
    }
}
