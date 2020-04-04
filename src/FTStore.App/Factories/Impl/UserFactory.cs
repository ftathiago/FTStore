using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories.Impl
{
    public class UserFactory : IUserFactory
    {
        public User Convert(UserRequest user)
        {
            if (user == null)
                return null;
            var userEntity = new User
            {
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Name = user.Name,
                Surname = user.Lastname,
                Password = user.Password
            };
            if (user.Id > 0)
                userEntity.DefineId(user.Id);
            return userEntity;
        }
    }
}
