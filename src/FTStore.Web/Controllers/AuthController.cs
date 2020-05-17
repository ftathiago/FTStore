using Microsoft.AspNetCore.Mvc;

using FTStore.Auth.App.Services;
using FTStore.Web.Models;
using FTStore.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace FTStore.Web.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthenticationService authenticationService,
            ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            var user = _authenticationService.AuthenticateBy(userLogin.Email, userLogin.Password);
            if (user == null)
            {
                var errorMessage = "User or password is invalid";
                return BadRequest(new { errorMessage });
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                user = user,
                token = token
            });
        }
    }
}