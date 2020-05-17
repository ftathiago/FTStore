using AutoMapper;
using FTStore.Auth.Domain.Entities;
using FTStore.Auth.Infra.Tables;

namespace FTStore.Auth.Infra.Mappings.Profiles
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
