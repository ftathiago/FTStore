using FTStore.App.Models;

namespace FTStore.App.Services
{
    public interface IUserService : IServiceBase
    {
        UserRequest Save(UserRequest user);
        UserRequest Authenticate(string email, string password);
    }
}
