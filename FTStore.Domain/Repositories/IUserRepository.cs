using FTStore.Domain.Entity;

namespace FTStore.Domain.Repository
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        UserEntity GetByIdentity(string email, string senha);
        UserEntity GetByEmail(string email);
    }
}