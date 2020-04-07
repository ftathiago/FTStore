using System.Collections.Generic;

using FTStore.UserDomain.Models;
using FTStore.UserDomain.ValueObjects;

namespace FTStore.UserDomain.Services
{
    public interface IAuthenticationService
    {
        UserAuthenticateResponse AuthenticateBy(Credentials credentials);
        IEnumerable<string> GetUserClaims(int id);
    }
}
