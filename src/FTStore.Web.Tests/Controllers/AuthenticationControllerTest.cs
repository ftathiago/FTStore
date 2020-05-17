using System.Net;

using Microsoft.AspNetCore.Mvc;

using FluentAssertions;

using FTStore.Auth.App.Models;
using FTStore.Auth.App.Services;
using FTStore.Web.Controllers;
using FTStore.Web.Services;
using FTStore.Web.Tests.Fixtures;

using Moq;

using Xunit;

namespace FTStore.Web.Tests.Controllers
{
    public class AuthenticationControllerTest : IClassFixture<AuthenticationControllerFixture>
    {
        private readonly AuthenticationControllerFixture _authFixture;

        public AuthenticationControllerTest(AuthenticationControllerFixture authFixture)
        {
            _authFixture = authFixture;
        }

        [Fact]
        private void ShouldAuthenticateValidUser()
        {
            var userLogin = _authFixture.getValidUserLogin();
            var authenticatedUser = _authFixture.getAuthenticatedUser();
            var authService = new Mock<IAuthenticationService>();
            authService
                .Setup(auth => auth.AuthenticateBy(userLogin.Email, userLogin.Password))
                .Returns(authenticatedUser);
            var tokenService = new Mock<ITokenService>();
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var authController = new AuthController(authService.Object, tokenService.Object);

            var response = authController.Authenticate(userLogin);

            var result = response.As<OkObjectResult>();
            result.StatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public void ShouldNotAuthenticateWhenUserIsInvalid()
        {
            var invalidLogin = _authFixture.getInvalidUserLogin();
            var authService = new Mock<IAuthenticationService>();
            authService
                .Setup(auth => auth.AuthenticateBy(invalidLogin.Email, invalidLogin.Password))
                    .Returns((AuthenticatedUser)null);
            var tokenService = new Mock<ITokenService>();
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;
            var authController = new AuthController(authService.Object, tokenService.Object);

            var response = authController.Authenticate(invalidLogin);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(expectedStatusCode);
        }
    }
}