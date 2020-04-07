using System.Collections.Generic;
using FTStore.UserDomain.Entities;
using FTStore.UserDomain.ValueObjects;

namespace FTStore.UserDomain.Repositories
{
    public interface IUserRepository
    {
        Credentials GetCredentialsBy(string email);
        FTStore.UserDomain.Entities.User GetByEmail(string email);

        IEnumerable<string> GetUserClaims(int id);
    }
}
