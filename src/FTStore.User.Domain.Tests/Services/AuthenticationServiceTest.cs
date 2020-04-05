
using FluentAssertions;
using FTStore.User.Domain.Services;
using FTStore.User.Domain.Services.Impl;
using FTStore.User.Domain.ValueObjects;
using Xunit;

namespace FTStore.User.Domain.Tests.Services
{
    public class AuthenticationServiceTest
    {
        [Fact]
        public void ShouldCreateAuthenticationService()
        {
            IAuthenticationService authentication = new AuthenticationService();

            authentication.Should().NotBeNull();
        }

        [Fact]
        public void ShouldAuthenticateWithCredentials()
        {
            Credentials credentials = new Credentials("email@email.com.br", "senha");
            IAuthenticationService authentication = new AuthenticationService();

            var userAuthenticated = authentication.AuthenticateBy(credentials);

            userAuthenticated.Should().NotBeNull();
        }
    }
}
