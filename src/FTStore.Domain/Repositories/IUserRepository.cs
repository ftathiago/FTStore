using FTStore.Domain.Entities;
using FTStore.Domain.ValueObjects;

namespace FTStore.Domain.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
        User GetByCredentials(Credentials credentials);
    }
}
