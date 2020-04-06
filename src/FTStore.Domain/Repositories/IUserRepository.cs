using FTStore.Domain.Entities;
using FTStore.Domain.ValueObjects;
using FTStore.Domain.Common.Repositories;

namespace FTStore.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
        User GetByCredentials(Credentials credentials);
    }
}
