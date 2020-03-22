namespace FTStore.Domain.ValueObjects
{
    public class Credentials
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Credentials(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
