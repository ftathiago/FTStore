
using System.Collections.Generic;
using FluentAssertions;

using FTStore.UserDomain.Entities;
using FTStore.UserDomain.Models;
using FTStore.UserDomain.Repositories;
using FTStore.UserDomain.Services;
using FTStore.UserDomain.Services.Impl;
using FTStore.UserDomain.ValueObjects;

using Moq;

using Xunit;

namespace FTStore.UserDomain.Tests.Services
{
    public class AuthenticationServiceTest
    {
        private const string EMAIL = "admin@admin.com";
        private const string SECRET_PHRASE = "swordfish";
        private const int ID = 1;
        private const string CREDENTIALS_ERROR = "User or password is invalid";

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
            userRepository
                .Setup(x => x.GetUserClaims(ID))
                .Returns(new List<string> { "ADMIN" })
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(credentials);

            userAuthenticated.Should().NotBeNull();
            userAuthenticated.Should().BeEquivalentTo(expectedUserAuthenticated);
            userRepository.Verify();
        }

        [Fact]
        public void ShoulAuthenticationBeInvalidWhenNoCredentials()
        {
            Credentials credentials = new Credentials(EMAIL, SECRET_PHRASE);
            var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepository
                .Setup(ur => ur.GetCredentialsBy(credentials.Email))
                .Returns((Credentials)null)
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(credentials);

            userAuthenticated.Should().BeNull();
            authentication.IsValid.Should().BeFalse();
            authentication.GetErrorMessages().Should().Be(CREDENTIALS_ERROR);
            userRepository.Verify();
        }

        [Fact]
        public void ShoulAuthenticationBeInvalidWhenCredentialsIsWrong()
        {
            Credentials credentials = new Credentials(EMAIL, SECRET_PHRASE);
            Credentials invalidCredentials = new Credentials(EMAIL, "wrong");
            var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepository
                .Setup(ur => ur.GetCredentialsBy(invalidCredentials.Email))
                .Returns((Credentials)invalidCredentials)
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(credentials);

            userAuthenticated.Should().BeNull();
            authentication.IsValid.Should().BeFalse();
            authentication.GetErrorMessages().Should().Be(CREDENTIALS_ERROR);
            userRepository.Verify();
        }
    }
}