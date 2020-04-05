
using FluentAssertions;
using FTStore.User.Domain.Model;
using FTStore.User.Domain.Repository;
using FTStore.User.Domain.Services;
using FTStore.User.Domain.Services.Impl;
using FTStore.User.Domain.ValueObjects;
using Moq;
using Xunit;

namespace FTStore.User.Domain.Tests.Services
{
    public class AuthenticationServiceTest
    {
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
            Credentials credentials = new Credentials("admin@admin.com.br", "password");
            var user = new UserAuthenticateResponse
            {
                Id = 1,
                Name = "User full name",
                EMail = "admin@admin.com"
            };
            user.Claims.Add("Admin");
            var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepository
                .Setup(ur => ur.AuthenticateBy(credentials))
                .Returns(user)
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(credentials);

            userAuthenticated.Should().NotBeNull();
            userAuthenticated.Should().BeEquivalentTo(user);
            userRepository.Verify();
        }
    }
}
