using FTStore.App.Models;

namespace FTStore.App.Services
{
    public interface IUserService : IServiceBase
    {
        User Save(User user);
        User Authenticate(string email, string password);
    }
}