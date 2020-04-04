using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories
{
    public interface IUserFactory
    {
        User Convert(UserRequest user);
    }
}
