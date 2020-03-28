using FTStore.Domain.Entities;
using FTStore.Domain.ValueObjects;

namespace FTStore.Domain.Repository
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        UserEntity GetByEmail(string email);
        UserEntity GetByCredentials(Credentials credentials);
    }
}
