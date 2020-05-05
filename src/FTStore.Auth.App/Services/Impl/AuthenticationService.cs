using FTStore.Lib.Common.Services;
using FTStore.Auth.App.Models;
using FTStore.Auth.Domain.Repositories;
using FTStore.Auth.Domain.ValueObjects;
using FTStore.Auth.Domain.Entities;

namespace FTStore.Auth.App.Services.Impl
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private const string CREDENTIALS_ERRORS = "User or password is invalid";
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthenticatedUser AuthenticateBy(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (!ValidateCredentials(user, password))
            {
                return null;
            }

            var userAuthenticateResponse = BuildAuthenticatedUser(user);

            return userAuthenticateResponse;
        }

        private bool ValidateCredentials(User user, string password)
        {
            if (user == null)
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return false;
            }

            var credentials = new Credentials(user.EMail,
                password, user.Password.Salt);

            if (!user.IsValidCredentials(credentials))
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return false;
            }
            return true;
        }

        private AuthenticatedUser BuildAuthenticatedUser(User user)
        {
            var userAuthenticateResponse = new AuthenticatedUser
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
