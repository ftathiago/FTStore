using AutoMapper;
using FTStore.Infra.Mappings.Profiles;

namespace FTStore.Infra.Tests.Fixtures
{
    public class AutoMapperFixture
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperFixture()
        {
            var _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMapProfile>();
                cfg.AddProfile<ProductMapProfile>();
                cfg.AddProfile<OrderMapProfile>();
                cfg.AddProfile<CustomerMapProfile>();
                cfg.AddProfile<PaymentMethodMapProfile>();
            });
            Mapper = _mapperConfig.CreateMapper();
        }
    }
}
