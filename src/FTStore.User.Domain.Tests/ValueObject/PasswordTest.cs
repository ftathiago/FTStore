using FluentAssertions;
using FTStore.User.Domain.ValueObjects;
using Xunit;

namespace FTStore.User.Domain.Tests.ValueObject
{
    public class PasswordTest
    {
        private const string SECRET_PHRASE = "swordfish";
        [Fact]
        public void ShouldCreatePasswordWithSecretPhrase()
        {
            var password = new Password(SECRET_PHRASE);

            password.Should().NotBeNull();
            password.Hash.Should().NotBeEmpty();
            password.Hash.Should().NotBeEmpty();
        }

        [Fact]
        public void ShouldCreatePasswordWithHashAndSalt()
        {
            byte[] hash = { 255, 255, 255, 255 };
            byte[] salt = { 255, 255, 255, 255 };

            var password = new Password(hash, salt);

            password.Should().NotBeNull();
        }

        [Fact]
        public void ShouldBeEqualWithSameHashAndSalt()
        {
            byte[] hash = { 255, 255, 255, 255 };
            byte[] salt = { 255, 255, 255, 255 };
            var password = new Password(hash, salt);
            var password2 = new Password(hash, salt);

            var equals = password.Equals(password2);

            equals.Should().BeTrue();
        }

        [Fact]
        public void ShouldBeDifferentWhenHashIsDifferent()
        {
            byte[] hash = { 255, 255, 255, 255 };
            byte[] diffHash = { 255, 255, 255, 254 };
            byte[] salt = { 255, 255, 255, 255 };
            var password = new Password(hash, salt);
            var password2 = new Password(diffHash, salt);

            var equals = password.Equals(password2);

            equals.Should().BeFalse();
        }
        [Fact]
        public void ShouldBeDifferentWhenSaltIsDifferent()
        {
            byte[] hash = { 255, 255, 255, 255 };
            byte[] salt = { 255, 255, 255, 254 };
            byte[] diffSalt = { 255, 255, 255, 255 };
            var password = new Password(hash, salt);
            var password2 = new Password(hash, diffSalt);

            var equals = password.Equals(password2);

            equals.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeDifferentWithOnlySameSecretPhrase()
        {
            var password = new Password(SECRET_PHRASE);
            var password2 = new Password(SECRET_PHRASE);

            var equals = password.Equals(password2);

            equals.Should().BeFalse();
        }
    }
}
