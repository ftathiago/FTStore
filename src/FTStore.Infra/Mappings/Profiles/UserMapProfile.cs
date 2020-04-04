using AutoMapper;
using FTStore.Domain.Entities;
using FTStore.Infra.Table;

namespace FTStore.Infra.Mappings.Profiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserTable>();
            CreateMap<UserTable, User>();
        }
    }
}
