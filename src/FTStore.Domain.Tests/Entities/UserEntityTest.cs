using FluentAssertions;
using FTStore.Domain.Entities;
using Xunit;

namespace FTStore.Domain.Tests.Entities
{
    public class UserEntityTest
    {
        [Fact]
        public void ShouldCreateUserEntity()
        {
            var user = new User();

            user.Should().NotBeNull();
        }
    }
}
