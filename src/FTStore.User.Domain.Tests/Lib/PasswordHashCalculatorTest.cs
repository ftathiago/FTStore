using FluentAssertions;
using FTStore.User.Domain.Lib;
using Xunit;

namespace FTStore.User.Domain.Tests.Lib
{
    public class PasswordHashCalculatorTest
    {
        [Fact]
        public void ShouldNeverCalculateSameHashAndSaltToSamePassword()
        {
            var password = "swordfish";
            var password1 = new PasswordHashCalculator(password);
            var password2 = new PasswordHashCalculator(password);

            password1.PasswordHash.Should().NotBeEquivalentTo(password2.PasswordHash);
            password1.PasswordSalt.Should().NotBeEquivalentTo(password2.PasswordSalt);
        }

        [Fact]
        public void ShouldBeEqualWhenUseTheKey()
        {
            var password = "swordfish";
            var password1 = new PasswordHashCalculator(password);

            var key = password1.PasswordSalt;
            var regeneratedPassword = new PasswordHashCalculator(password, key);

            password1.Should().BeEquivalentTo(regeneratedPassword);
        }
    }
}
