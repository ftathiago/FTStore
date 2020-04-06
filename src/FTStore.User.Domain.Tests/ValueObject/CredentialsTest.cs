using FluentAssertions;
using FTStore.User.Domain.ValueObjects;
using Xunit;

namespace FTStore.User.Domain.Tests.ValueObject
{
    public class CredentialsTest
    {
        private const string SECRET_PHRASE = "swordfish";
        private const string EMAIL = "admin@admin.com";

        [Fact]
        public void ShouldCreateWithEMailAndSecretPhrase()
        {
            var credentials = new Credentials(EMAIL, SECRET_PHRASE);

            credentials.Should().NotBeNull();
            credentials.Email.Should().Be(EMAIL);
            credentials.Password.Should().NotBeNull();
        }

        [Fact]
        public void ShouldCreateWithEmailAndPassword()
        {
            var password = new Password(SECRET_PHRASE);

            var credentials = new Credentials(EMAIL, password);

            credentials.Email.Should().Be(EMAIL);
            credentials.Password.Should().Be(password);
        }
    }
}
