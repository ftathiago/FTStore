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
            var userCredentials = _userRepository.GetCredentialsBy(credentials.Email);

            if (userCredentials == null)
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return null;
            }

            if (!credentials.Equals(userCredentials))
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return null;
            }

            var user = _userRepository.GetByEmail(credentials.Email);

            var userAuthenticateResponse = new UserAuthenticateResponse
            {
                Id = user.Id,
                Name = user.Name,
                EMail = user.EMail
            };
            GetUserClaims(userAuthenticateResponse.Id).ToList().ForEach(claim =>
                userAuthenticateResponse.Claims.Add(claim)
            );


            return userAuthenticateResponse;
        }

        public IEnumerable<string> GetUserClaims(int id)
        {
            return _userRepository.GetUserClaims(id);
        }
    }
}
