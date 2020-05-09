using FTStore.Auth.App.Services;
using FTStore.Web.Models;
using FTStore.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FTStore.Web.Controllers
{
    [Route("api/[Controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private ITokenService _tokenService;

        public AuthenticateController(IAuthenticationService authenticationService,
            ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            var user = _authenticationService.AuthenticateBy(userLogin.Email, userLogin.Password);
            if (user == null)
            {
                var errorMessage = _authenticationService.GetErrorMessages();
                return BadRequest(new { errorMessage = errorMessage });
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