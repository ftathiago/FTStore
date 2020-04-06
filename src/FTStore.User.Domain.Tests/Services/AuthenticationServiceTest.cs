
using FluentAssertions;

using FTStore.User.Domain.Models;
using FTStore.User.Domain.Repositories;
using FTStore.User.Domain.Services;
using FTStore.User.Domain.Services.Impl;
using FTStore.User.Domain.ValueObjects;

using Moq;

using Xunit;

namespace FTStore.User.Domain.Tests.Services
{
    public class AuthenticationServiceTest
    {
        private const string EMAIL = "admin@admin.com";
        private const string SECRET_PHRASE = "swordfish";
        private const int ID = 1;
        [Fact]
        public void ShouldCreateAuthenticationService()
        {
            var userRepository = new Mock<IUserRepository>();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            authentication.Should().NotBeNull();
        }

        [Fact]
        public void ShouldAuthenticateWithCredentials()
        {
            Credentials credentials = new Credentials(EMAIL, SECRET_PHRASE);
            var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var user = new User("Name", "Surname", EMAIL, credentials.Password);
            user.DefineId(ID);
            var expectedUserAuthenticated = new UserAuthenticateResponse
            {
                Id = ID,
                Name = user.Name,
                EMail = user.EMail
            };
            expectedUserAuthenticated.Claims.Add("ADMIN");
            userRepository
                .Setup(ur => ur.GetCredentialsBy(credentials.Email))
                .Returns(credentials)
                .Verifiable();
            userRepository
                .Setup(ur => ur.GetByEmail(credentials.Email))
                .Returns(user)
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(credentials);

            userAuthenticated.Should().NotBeNull();
            userAuthenticated.Should().BeEquivalentTo(expectedUserAuthenticated);
            userRepository.Verify();
        }
    }
}
