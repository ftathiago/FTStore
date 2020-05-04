using System.Collections.Generic;
using FTStore.UserDomain.Entities;
using FTStore.UserDomain.ValueObjects;

namespace FTStore.UserDomain.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
    }
}
