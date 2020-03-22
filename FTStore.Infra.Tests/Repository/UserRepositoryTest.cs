using FTStore.Domain.Entities;
using FTStore.Infra.Repository;
using FTStore.Infra.Tests.Fixture;
using FluentAssertions;
using Xunit;
using FTStore.Domain.ValueObjects;

namespace FTStore.Infra.Tests.Repository
{
    public class UserRepositoryTest : IClassFixture<ContextFixture>
    {
        private ContextFixture _fixture { get; }
        private const string EXISTING_EMAIL = "admin@admin.com";
        private const string NOT_EXISTING_EMAIL = "not@exist.com";
        private const string VALID_PASSWORD = "v4L1dP455w0rD";

        private const string INVALID_PASSWORD = "123";

        public UserRepositoryTest(ContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldFindUserByEMail()
        {
            using var context = _fixture.Ctx;
            using var repository = new UserRepository(context);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL
            };
            repository.Register(user);

            var foundedUser = repository.GetByEmail(EXISTING_EMAIL);

            foundedUser.Should().Be(user);
        }

        [Fact]
        public void ShouldNotFindUserInexistentEmail()
        {
            using var context = _fixture.Ctx;
            using var repository = new UserRepository(context);
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
            using var context = _fixture.Ctx;
            using var repository = new UserRepository(context);
            Credentials credentials = new Credentials(EXISTING_EMAIL, VALID_PASSWORD);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL,
                Password = VALID_PASSWORD
            };
            repository.Register(user);

            var registeredUser = repository.GetByCredentials(credentials);

            registeredUser.Should().Be(user);
        }

        [Fact]
        public void ShouldNotFoundUserWithWrongEmail()
        {
            using var context = _fixture.Ctx;
            using var repository = new UserRepository(context);
            Credentials credentials = new Credentials(NOT_EXISTING_EMAIL, VALID_PASSWORD);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL,
                Password = VALID_PASSWORD
            };
            repository.Register(user);

            var registeredUser = repository.GetByCredentials(credentials);

            registeredUser.Should().Be(null);
        }

        [Fact]
        public void ShouldNotFoundUserWithWrongPassword()
        {
            using var context = _fixture.Ctx;
            using var repository = new UserRepository(context);
            Credentials credentials = new Credentials(EXISTING_EMAIL, INVALID_PASSWORD);
            UserEntity user = new UserEntity()
            {
                Email = EXISTING_EMAIL,
                Password = VALID_PASSWORD
            };
            repository.Register(user);

            var registeredUser = repository.GetByCredentials(credentials);

            registeredUser.Should().Be(null);
        }
    }
}
