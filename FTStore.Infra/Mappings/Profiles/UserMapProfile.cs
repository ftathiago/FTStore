using AutoMapper;
using FTStore.Domain.Entities;
using FTStore.Infra.Model;

namespace FTStore.Infra.Mappings.Profiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
        }
    }
}
