using FTStore.App.Models;
using FTStore.Domain.Entity;

namespace FTStore.App.Factories
{
    public interface IUserFactory
    {
        UserEntity Convert(User user);
    }
}