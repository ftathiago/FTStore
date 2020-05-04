using System.Collections.Generic;
using FTStore.Lib.Common.Services;
using FTStore.Auth.Domain.Models;
using FTStore.Auth.Domain.ValueObjects;

namespace FTStore.Auth.Domain.Services
{
    public interface IAuthenticationService : IServiceBase
    {
        UserAuthenticateResponse AuthenticateBy(Credentials credentials);
    }
}
