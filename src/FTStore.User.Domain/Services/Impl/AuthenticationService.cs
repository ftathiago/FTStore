using FTStore.User.Domain.Model;
using FTStore.User.Domain.Repository;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserAuthenticateResponse AuthenticateBy(Credentials credentials)
        {
            var userCredentials = _userRepository.GetCredentialsBy(credentials.Email);

            if (userCredentials == null)
                return null;

            if (!credentials.Equals(userCredentials))
                return null;

            var user = _userRepository.GetByEmail(credentials.Email);

            var userAuthenticateResponse = new UserAuthenticateResponse
            {
                Id = user.Id,
                Name = user.Name,
                EMail = user.EMail
            };
            userAuthenticateResponse.Claims.Add("ADMIN");


            return userAuthenticateResponse;
        }
    }
}
