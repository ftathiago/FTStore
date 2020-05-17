using FTStore.Lib.Common.Services;
using FTStore.Auth.App.Models;

namespace FTStore.Auth.App.Services
{
    public interface IAuthenticationService : IServiceBase
    {
        AuthenticatedUser AuthenticateBy(string email, string password);
    }
}
