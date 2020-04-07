using System.Collections.Generic;

using FTStore.User.Domain.Models;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain.Services
{
    public interface IAuthenticationService
    {
        UserAuthenticateResponse AuthenticateBy(Credentials credentials);
        IEnumerable<string> GetUserClaims(int id);
    }
}