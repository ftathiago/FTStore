using System.Collections.Generic;
using FTStore.Domain.Repository;
using FTStore.Domain.Entity;
using FTStore.Infra.Context;
using System.Linq;

namespace FTStore.Infra.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(FTStoreDbContext FTStoreContexto) : base(FTStoreContexto)
        {
        }

        public UserEntity GetByIdentity(string email, string senha)
        {
            return FTStoreDbContext.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Password == senha);
        }

        public UserEntity GetByEmail(string email)
        {
            return FTStoreDbContext.Usuarios
                .FirstOrDefault(u => u.Email == email);
        }
    }
}