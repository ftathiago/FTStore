using AutoMapper;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Tables;

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
