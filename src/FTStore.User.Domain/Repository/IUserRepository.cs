using FTStore.User.Domain.Model;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain.Repository
{
    public interface IUserRepository
    {
        Credentials GetCredentialsBy(string email);
        User GetByEmail(string email);
    }
}
