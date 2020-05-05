using FTStore.Auth.App.Models;

namespace FTStore.Web.Services
{
    public interface ITokenService
    {
        string GenerateToken(AuthenticatedUser user);
    }
}
