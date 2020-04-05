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
            return _userRepository.AuthenticateBy(credentials);
        }
    }
}
