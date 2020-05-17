using System.Collections.Generic;

using FluentAssertions;

using FTStore.Auth.App.Models;
using FTStore.Auth.App.Services;
using FTStore.Auth.App.Services.Impl;
using FTStore.Auth.Domain.Entities;
using FTStore.Auth.Domain.Repositories;
using FTStore.Auth.Domain.ValueObjects;

using Moq;

using Xunit;

namespace FTStore.Auth.Domain.Tests.Services
{
    public class AuthenticationServiceTest
    {
        private const string EMAIL = "admin@admin.com";
        private const string EMAIL_INVALID = "foo@bar.com";
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
        public void ShouldAuthenticateWithValidCredentials()
        {
            var password = new Password(SECRET_PHRASE);
            var userClaims = "ADMIN";
            var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var user = new User("Name", "Surname", EMAIL, password);
            user.Claims.Add(userClaims);
            user.DefineId(ID);
            var expectedUserAuthenticated = new AuthenticatedUser
            {
                Id = ID,
                Name = user.Name,
                EMail = user.Email
            };
            expectedUserAuthenticated.Claims.Add("ADMIN");
            userRepository
                .Setup(ur => ur.GetByEmail(EMAIL))
                .Returns(user)
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(EMAIL, SECRET_PHRASE);

            userAuthenticated.Should().NotBeNull();
            userAuthenticated.Should().BeEquivalentTo(expectedUserAuthenticated);
            userRepository.Verify();
        }

        [Fact]
        public void ShoulNoAuthenticateWhenEmailIsInvalid()
        {
            var password = new Password(SECRET_PHRASE);
            var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepository
                .Setup(ur => ur.GetByEmail(EMAIL_INVALID))
                .Returns((User)null)
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(EMAIL_INVALID, SECRET_PHRASE);

            userAuthenticated.Should().BeNull();
            authentication.IsValid.Should().BeFalse();
            authentication.GetErrorMessages().Should().Be(CREDENTIALS_ERROR);
            userRepository.Verify();
        }

        [Fact]
        public void ShoulAuthenticationBeInvalidWhenPasswordIsWrong()
        {
            const string PASSWORD_INVALID = "wROng";
            var password = new Password(SECRET_PHRASE);
            var user = new User("Name", "Surname", EMAIL, password);
            user.Claims.Add("ADMIN");
            user.DefineId(ID);
            var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepository
                .Setup(ur => ur.GetByEmail(EMAIL))
                .Returns(user)
                .Verifiable();
            IAuthenticationService authentication = new AuthenticationService(userRepository.Object);

            var userAuthenticated = authentication.AuthenticateBy(EMAIL, PASSWORD_INVALID);

            userAuthenticated.Should().BeNull();
            authentication.IsValid.Should().BeFalse();
            authentication.GetErrorMessages().Should().Be(CREDENTIALS_ERROR);
            userRepository.Verify();
        }
    }
}
