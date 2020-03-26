using AutoMapper;
using FTStore.Infra.Mappings.Profiles;

namespace FTStore.Infra.Tests.Fixture
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
            });
            Mapper = _mapperConfig.CreateMapper();
        }
    }
}
