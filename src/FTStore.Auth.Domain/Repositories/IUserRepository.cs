using System.Collections.Generic;
using FTStore.Auth.Domain.Entities;
using FTStore.Auth.Domain.ValueObjects;

namespace FTStore.Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
    }
}
