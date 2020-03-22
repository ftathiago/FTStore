using Xunit;
using FluentAssertions;
using FTStore.Domain.Entities;

namespace FTStore.Domain.Tests.Entities
{
    public class UserEntityTest
    {
        [Fact]
        public void ShouldCreateUser()
        {
            var userEntity = new UserEntity();

            userEntity.Should().NotBe(null);
        }
    }
}
