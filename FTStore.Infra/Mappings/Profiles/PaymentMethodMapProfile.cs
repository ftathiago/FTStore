using AutoMapper;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Model;

namespace FTStore.Infra.Mappings.Profiles
{
    public class PaymentMethodMapProfile : Profile
    {
        public PaymentMethodMapProfile()
        {
            CreateMap<PaymentMethod, PaymentMethodModel>();
            CreateMap<PaymentMethodModel, PaymentMethod>();
        }
    }
}
