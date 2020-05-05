using FTStore.Lib.Common.Services;
using FTStore.Auth.App.Models;
using FTStore.Auth.Domain.ValueObjects;

namespace FTStore.Auth.App.Services
{
    public interface IAuthenticationService : IServiceBase
    {
        AuthenticatedUser AuthenticateBy(string email, string password);
    }
}
