using FTStore.User.Domain.Model;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        public UserAuthenticateResponse AuthenticateBy(Credentials credentials)
        {
            return new UserAuthenticateResponse();
        }
    }
}
