using FTStore.Domain.Repository;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;
using System.Linq;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Table;
using AutoMapper;
using FTStore.Infra.Common.Repository;

namespace FTStore.Infra.Repository
{
    public class UserRepository : BaseRepository<User, UserTable>, IUserRepository
    {
        public UserRepository(FTStoreDbContext ftStoreContext, IMapper mapper)
            : base(ftStoreContext, mapper) { }

        public User GetByEmail(string email)
        {
            var data = DbSet.FirstOrDefault(u => u.Email == email);
            if (data == null)
                return null;
            return _mapper.Map<User>(data);
        }

        public User GetByCredentials(Credentials credentials)
        {
            var hash = credentials.Hash();
            var salt = credentials.Salt();

            var data = DbSet.SingleOrDefault(user =>
                user.Email == credentials.Email
                && user.Hash.SequenceEqual(hash)
                && user.Salt.SequenceEqual(salt));
            if (data == null)
                return null;

            return _mapper.Map<User>(data);
        }
    }
}
