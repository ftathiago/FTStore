using System.Collections.Generic;
using FTStore.Lib.Common.Services;
using FTStore.UserDomain.Models;
using FTStore.UserDomain.ValueObjects;

namespace FTStore.UserDomain.Services
{
    public interface IAuthenticationService : IServiceBase
    {
        UserAuthenticateResponse AuthenticateBy(Credentials credentials);
        IEnumerable<string> GetUserClaims(int id);
    }
}
