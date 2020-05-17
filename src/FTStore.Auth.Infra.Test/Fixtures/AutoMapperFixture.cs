using AutoMapper;
using FTStore.Auth.Infra.Mappings.Profiles;

namespace FTStore.Auth.Infra.Tests.Fixtures
{
    public class AutoMapperFixture
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperFixture()
        {
            var _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMapProfile>();
            });
            Mapper = _mapperConfig.CreateMapper();
        }
    }
}
