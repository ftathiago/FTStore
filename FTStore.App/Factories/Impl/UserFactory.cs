using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories.Impl
{
    public class UserFactory : IUserFactory
    {
        public UserEntity Convert(User user)
        {
            if (user == null)
                return null;
            return new UserEntity
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Name = user.Name,
                Surname = user.Lastname,
                Password = user.Password
            };
        }
    }
}
