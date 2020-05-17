using System.Linq;

using AutoMapper;

using FTStore.Auth.Domain.Entities;
using FTStore.Auth.Domain.Repositories;
using FTStore.Auth.Infra.Tables;
using FTStore.Infra.Context;
using FTStore.Lib.Common.Repositories;

namespace FTStore.Auth.Infra.Repositories
{
    public class UserRepository : BaseRepository<User, UserTable>, IUserRepository
    {
        public UserRepository(FTStoreAuthContext ftStoreAuthContext, IMapper mapper)
          : base(ftStoreAuthContext, mapper) { }
        public User GetByEmail(string email)
        {
            var user = DbSet.Where(user => user.Email == email).FirstOrDefault();
            User userEntity = _mapper.Map<UserTable, User>(user);
            return userEntity;
        }
    }
}