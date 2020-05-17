using FTStore.Auth.Domain.Entities;
using FTStore.Auth.Domain.Repositories;
using FTStore.Auth.Domain.ValueObjects;

namespace FTStore.Auth.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetByEmail(string email)
        {
            var password = new Password("swordfish");
            var user = new User("user name", "surname", "admin@admin.com", password);
            user.Claims.Add("Teste");
            return user;
        }
    }
}