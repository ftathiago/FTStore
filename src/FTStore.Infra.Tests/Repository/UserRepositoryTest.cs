using FTStore.Domain.Entities;
using FTStore.Infra.Repository;
using FTStore.Infra.Tests.Fixture;
using FluentAssertions;
using Xunit;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Model;
using System.Text;
using FTStore.Infra.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FTStore.Infra.Tests.Repository
{
    public class UserRepositoryTest : BaseRepositoryTest<UserModel>
    {
        private const int ID = 1;
        private const string EXISTING_EMAIL = "admin@admin.com";
        private const string NOT_EXISTING_EMAIL = "not@exist.com";
        private const string PASSWORD = "P455W0RD";
        private const string INVALID_PASSWORD = "123";
        private const string SALT = "5ALT";
        private const string INVALID_SALT = "123";
        private const bool IS_ADMIN = true;

        public UserRepositoryTest(ContextFixture contextFixture, AutoMapperFixture autoMapperFixture)
            : base(contextFixture, autoMapperFixture)
        { }

        [Fact]
        public override void ShouldRegister()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL,
                IsAdmin = IS_ADMIN
            };
            user.DefineId(ID);
            context.Users.Should().BeEmpty();

            repository.Register(user);

            context.Users.Should().ContainSingle();
        }

        [Fact]
        public override void ShouldConserveDataAfterRegister()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL,
                IsAdmin = IS_ADMIN
            };
            user.DefineId(ID);
            repository.Register(user);

            var registeredUser = context.Users.First();

            registeredUser.Id.Should().Be(ID);
            registeredUser.IsAdmin.Should().Be(IS_ADMIN);
            registeredUser.Email.Should().BeSameAs(EXISTING_EMAIL);
        }

        [Fact]
        public override void ShouldUpdate()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            AddAtRepository(context, ID);
            UserEntity user = repository.GetById(ID);
            user.Email = NOT_EXISTING_EMAIL;

            repository.Update(user);

            var userModified = repository.GetById(ID);
            userModified.Email.Should().Be(NOT_EXISTING_EMAIL);
        }

        [Fact]
        public override void ShouldDeleteByEntityReference()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            AddAtRepository(context, ID);
            UserEntity user = new UserEntity();
            user.DefineId(ID);

            repository.Remove(user);

            var userDeleted = !context.Users.Any(user => user.Id == ID);
            userDeleted.Should().BeTrue();
        }

        [Fact]
        public void ShouldDeleteUserByEntityReference()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            AddAtRepository(context, ID);
            var user = new UserEntity();
            user.DefineId(ID);

            repository.Remove(user);

            context.Users.Should().BeEmpty();
        }

        [Fact]
        public void ShouldFindUserByEMail()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL
            };
            user.DefineId(1);
            repository.Register(user);

            var foundedUser = repository.GetByEmail(EXISTING_EMAIL);

            foundedUser.Should().Be(user);
        }

        [Fact]
        public void ShouldNotFindUserInexistentEmail()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL
            };
            repository.Register(user);

            var foundedUser = repository.GetByEmail(NOT_EXISTING_EMAIL);

            foundedUser.Should().Be(null);
        }

        [Fact]
        public void ShouldFoundUserByCredentials()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            var user = AddAtRepository(context);
            var expectedUserEntity = MapperFixture.Mapper.Map<UserEntity>(user);
            Credentials credentials = new Credentials(EXISTING_EMAIL, PASSWORD);

            var registeredUser = repository.GetByCredentials(credentials);

            registeredUser.Should().BeEquivalentTo(expectedUserEntity);
        }

        [Fact]
        public void ShouldNotFoundUserWithWrongEmail()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            var user = AddAtRepository(context);
            Credentials credentials = new Credentials(NOT_EXISTING_EMAIL, PASSWORD);
            var expectedUserEntity = MapperFixture.Mapper.Map<UserEntity>(user);

            var registeredUser = repository.GetByCredentials(credentials);

            registeredUser.Should().Be(null);
        }

        [Fact]
        public void ShouldNotFoundUserWithWrongPassword()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new UserRepository(context, MapperFixture.Mapper);
            AddAtRepository(context);
            Credentials credentials = new Credentials(EXISTING_EMAIL, INVALID_PASSWORD);

            var registeredUser = repository.GetByCredentials(credentials);

            registeredUser.Should().Be(null);
        }

        protected override UserModel GetModelPrototype(int id = 0)
        {
            var credentials = new Credentials(EXISTING_EMAIL, PASSWORD);
            var user = new UserModel
            {
                Id = id,
                Email = EXISTING_EMAIL,
                Hash = credentials.Hash(),
                Salt = credentials.Salt(),
                IsAdmin = true
            };
            return user;
        }
    }
}