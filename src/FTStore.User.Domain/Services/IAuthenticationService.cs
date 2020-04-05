using FTStore.User.Domain.Model;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain.Services
{
    public interface IAuthenticationService
    {
        UserAuthenticateResponse AuthenticateBy(Credentials credentials);
    }
}
