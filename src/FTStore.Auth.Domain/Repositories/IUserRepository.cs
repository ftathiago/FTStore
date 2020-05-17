using FTStore.Auth.Domain.Entities;

namespace FTStore.Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
    }
}
