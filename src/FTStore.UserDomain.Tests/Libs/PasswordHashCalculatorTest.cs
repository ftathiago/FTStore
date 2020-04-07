using FluentAssertions;
using FTStore.UserDomain.Libs;
using Xunit;

namespace FTStore.UserDomain.Tests.Libs
{
    public class PasswordHashCalculatorTest
    {
        [Fact]
        public void ShouldNeverCalculateSameHashAndSaltToSamePassword()
        {
            var password = "swordfish";
            var firstHashCalc = new PasswordHashCalculator(password);
            var newHashCalc = new PasswordHashCalculator(password);

            firstHashCalc.PasswordHash.Should().NotBeEquivalentTo(newHashCalc.PasswordHash);
            firstHashCalc.PasswordSalt.Should().NotBeEquivalentTo(newHashCalc.PasswordSalt);
        }

        [Fact]
        public void ShouldBeEqualWhenUseTheKey()
        {
            var password = "swordfish";
            var originalHashCalc = new PasswordHashCalculator(password);

            var key = originalHashCalc.PasswordSalt;
            var hashCalcWithKey = new PasswordHashCalculator(password, key);

            originalHashCalc.Should().BeEquivalentTo(hashCalcWithKey);
        }
    }
}
