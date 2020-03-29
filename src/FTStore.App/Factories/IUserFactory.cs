using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories
{
    public interface IUserFactory
    {
        UserEntity Convert(User user);
    }
}