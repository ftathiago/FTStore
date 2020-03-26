using FTStore.Domain.Repository;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;
using System.Linq;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Model;
using AutoMapper;

namespace FTStore.Infra.Repository
{
    public class UserRepository : BaseRepository<UserEntity, UserModel>, IUserRepository
    {
        public UserRepository(FTStoreDbContext ftStoreContext, IMapper mapper)
            : base(ftStoreContext, mapper) { }

        public UserEntity GetByEmail(string email)
        {
            var data = DbSet.FirstOrDefault(u => u.Email == email);
            if (data == null)
                return null;
            return _mapper.Map<UserEntity>(data);
        }

        public UserEntity GetByCredentials(Credentials credentials)
        {
            // var data = DbSet.SingleOrDefault(user =>
            //     user.Email == credentials.Email && user.Password == credentials.Password);
            // if (data == null)
            return null;
            // return _mapper.Map<UserEntity>(data);
        }
    }
}
