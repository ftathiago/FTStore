using FTStore.Auth.Domain.Entities;
using FTStore.Auth.Domain.ValueObjects;
using FTStore.Auth.Infra.Repositories;
using FTStore.Auth.Infra.Tables;
using FTStore.Auth.Infra.Test.Fixtures;
using FTStore.Auth.Infra.Tests.Fixtures;
using FTStore.Infra.Context;

using FluentAssertions;

using Xunit;
using System.Linq;

namespace FTStore.Auth.Infra.Test.Repositories
{
    public class UserRepositoryTest : BaseRepositoryTest<UserTable>
    {
        private Password _password;
        private const string EMAIL_FOUNDABLE = "admin@admin.com";
        private const string EMAIL_UNFOUNDABLE = "anything@unfoundable.com";

        public UserRepositoryTest(ContextFixture context, AutoMapperFixture mapper)
            : base(context, mapper)
        {
        }

        [Fact]
        public override void ShouldRegister()
        {
            var user = GetUser();
            using var context = Context.CreateNewDbContext();
            var userRepository = new UserRepository(context, Mapper.Mapper);

            userRepository.Register(user);

            context.Users.Should().ContainSingle();
        }

        [Fact]
        public override void ShouldConserveDataAfterRegister()
        {
            var user = GetUser();
            using var context = Context.CreateNewDbContext();
            var userRepository = new UserRepository(context, Mapper.Mapper);
            userRepository.Register(user);

            var registeredUser = userRepository.GetAll().FirstOrDefault();
            user.DefineId(registeredUser.Id);

            registeredUser.Should().BeEquivalentTo(user);
            registeredUser.Claims.Should().BeEquivalentTo(user.Claims);
        }

        [Fact]
        public override void ShouldUpdate()
        {
            const int USER_ID = 1;
            using var context = Context.CreateNewDbContext();
            AddAtRepository(context, USER_ID);
            var userRepository = new UserRepository(context, Mapper.Mapper);
            var userToUpdate = GetUser();
            userToUpdate.DefineId(USER_ID);
            userToUpdate.Claims.Clear();

            userRepository.Update(userToUpdate);

            UserTable userUpdated = context.Users.Find(USER_ID);

            userUpdated.IsAdmin.Should().BeFalse();
        }

        [Fact]
        public override void ShouldDeleteByEntityReference()
        {
            const int USER_ID = 1;
            using var context = Context.CreateNewDbContext();
            AddAtRepository(context, USER_ID);
            var userRepository = new UserRepository(context, Mapper.Mapper);
            var userToDelete = GetUser();
            userToDelete.DefineId(USER_ID);

            userRepository.Remove(userToDelete);

            UserTable userWasDeleted = context.Users.Find(USER_ID);

            userWasDeleted.Should().BeNull();
        }

        [Fact]
        public void ShoulLocalizeUserByEmailWhenEmailIsValid()
        {
            using var context = Context.CreateNewDbContext();
            AddAtRepository(context);
            var userRepository = new UserRepository(context, Mapper.Mapper);

            var userFound = userRepository.GetByEmail(EMAIL_FOUNDABLE);

            userFound.Should().NotBeNull();
            userFound.Email.Should().Be(EMAIL_FOUNDABLE);
        }

        [Fact]
        public void ShouldNotLocalizeUserByEmailWhenEmailIsInvalid()
        {
            using var context = Context.CreateNewDbContext();
            AddAtRepository(context);
            var userRepository = new UserRepository(context, Mapper.Mapper);

            var userFound = userRepository.GetByEmail(EMAIL_UNFOUNDABLE);

            userFound.Should().BeNull();
        }

        protected override UserTable GetModelPrototype(int id = 0)
        {
            var pass = GetPassword();
            return new UserTable
            {
                Id = id,
                Email = EMAIL_FOUNDABLE,
                Hash = pass.Hash,
                Salt = pass.Salt,
                IsAdmin = true
            };
        }

        private User GetUser()
        {
            var pass = GetPassword();
            var user = new User("User name", "Surname", EMAIL_FOUNDABLE, pass);
            user.Claims.Add("admin");
            return user;
        }

        private Password GetPassword()
        {
            if (_password == null)
                _password = new Password("swordfish");
            return _password;
        }
    }
}