using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using FTStore.Auth.Domain.Entities;
using FTStore.Auth.Infra.Tables;

namespace FTStore.Auth.Infra.Mappings.Profiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserTable>()
                .ForMember(
                    userTable => userTable.IsAdmin,
                    c => c.MapFrom(m => m.Claims.Contains("admin")
                )
            );
            CreateMap<UserTable, User>()
                .ForMember(
                    user => user.Claims,
                    opt => opt.MapFrom(
                        userTable => userTable.IsAdmin ?
                            new List<string> { "admin" }
                            : new List<string>())
                );
        }
    }
}
