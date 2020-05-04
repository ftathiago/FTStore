using FTStore.Lib.Common.Services;
using FTStore.Auth.Domain.Models;
using FTStore.Auth.Domain.ValueObjects;

namespace FTStore.Auth.App.Services
{
    public interface IAuthenticationService : IServiceBase
    {
        UserAuthenticateResponse AuthenticateBy(Credentials credentials);
    }
}
