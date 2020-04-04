using FTStore.Domain.Repository;
using FTStore.App.Models;
using FTStore.Domain.Entities;
using FTStore.Domain.ValueObjects;
using FTStore.App.Factories;
using System;
using System.Linq;

namespace FTStore.App.Services.Impl
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public UserService(IUserRepository userRepository,
            IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        public UserRequest Save(UserRequest user)
        {
            try
            {
                if (IsUserAlreadyRegistered(user))
                {
                    AddErrorMessage("User already registered");
                    return null;
                }

                UserEntity userEntity = _userFactory.Convert(user);
                if (!userEntity.IsValid())
                {
                    userEntity.ValidationResult.Errors.ToList().ForEach(error =>
                        AddErrorMessage(error.ErrorMessage));
                    return null;
                }
                _userRepository.Register(userEntity);
                user.Id = userEntity.Id;
                return user;
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                return null;
            }
        }

        public UserRequest Authenticate(string email, string password)
        {
            var credentials = new Credentials(email, password);
            var authenticatedUser = _userRepository.GetByCredentials(credentials);
            if (authenticatedUser == null)
            {
                AddErrorMessage("Invalid credentials");
                return null;
            }
            return new UserRequest
            {
                Id = authenticatedUser.Id,
                Name = authenticatedUser.Name,
                Lastname = authenticatedUser.Surname,
                Email = authenticatedUser.Email,
                IsAdmin = authenticatedUser.IsAdmin
            };
        }

        private bool IsUserAlreadyRegistered(UserRequest user)
        {
            var registeredUser = _userRepository.GetByEmail(user.Email);
            return registeredUser != null;
        }
    }
}
