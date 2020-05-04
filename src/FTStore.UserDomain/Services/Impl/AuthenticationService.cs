using System.Collections.Generic;
using System.Linq;
using FTStore.Lib.Common.Services;
using FTStore.UserDomain.Models;
using FTStore.UserDomain.Repositories;
using FTStore.UserDomain.ValueObjects;

namespace FTStore.UserDomain.Services.Impl
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserAuthenticateResponse AuthenticateBy(Credentials credentials)
        {
            const string CREDENTIALS_ERRORS = "User or password is invalid";
            var user = _userRepository.GetByEmail(credentials.Email);

            if (user == null)
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return null;
            }

            if (!user.IsValidCredentials(credentials))
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return null;
            }

            var userAuthenticateResponse = new UserAuthenticateResponse
            {
                Id = user.Id,
                Name = user.Name,
                EMail = user.EMail
            };
            userAuthenticateResponse.Claims.AddRange(user.Claims);

            return userAuthenticateResponse;
        }
    }
}
