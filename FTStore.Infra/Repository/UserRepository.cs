using FTStore.Domain.Repository;
using FTStore.Domain.Entity;
using FTStore.Infra.Context;
using System.Linq;
using FTStore.Domain.ValueObjects;

namespace FTStore.Infra.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(FTStoreDbContext ftStoreContext) : base(ftStoreContext)
        { }

        public UserEntity GetByEmail(string email)
        {
            return DbSet.FirstOrDefault(u => u.Email == email);
        }

        public UserEntity GetByCredentials(Credentials credentials)
        {
            return DbSet.SingleOrDefault(user =>
                user.Email == credentials.Email && user.Password == credentials.Password);
        }
    }
}
