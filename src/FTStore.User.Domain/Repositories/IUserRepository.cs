using FTStore.User.Domain.Models;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Credentials GetCredentialsBy(string email);
        User GetByEmail(string email);
    }
}
