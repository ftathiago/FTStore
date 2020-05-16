using System.Net;

using Microsoft.AspNetCore.Mvc;

using Faker;

using FluentAssertions;

using FTStore.Auth.App.Models;
using FTStore.Auth.App.Services;
using FTStore.Web.Controllers;
using FTStore.Web.Models;
using FTStore.Web.Services;

using Moq;

using Xunit;

namespace FTStore.Web.Tests.Controllers
{
    public class AuthenticationControllerTest
    {
        const string EMAIL = "admin@admin.com";
        const string PASS = "swordfish";

        [Fact]
        private void ShouldAuthenticateValidUser()
        {
            var authenticatedUser = new AuthenticatedUser
            {
                Id = 1,
                Name = Faker.Name.FullName()
            };
            var authService = new Mock<IAuthenticationService>();
            authService
                .Setup(auth => auth.AuthenticateBy(EMAIL, PASS))
                .Returns(authenticatedUser);
            var tokenService = new Mock<ITokenService>();
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var authController = new AuthenticateController(authService.Object, tokenService.Object);
            var userLogin = new UserLogin
            {
                Email = EMAIL,
                Password = PASS
            };

            var response = authController.Authenticate(userLogin);

            response.As<OkObjectResult>().StatusCode.Should().Be(expectedStatusCode);
        }
    }
}