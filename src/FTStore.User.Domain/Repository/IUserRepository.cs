using FTStore.User.Domain.Model;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain.Repository
{
    public interface IUserRepository
    {
        UserAuthenticateResponse AuthenticateBy(Credentials credentials);
    }
}
