using FTStore.Auth.App.Models;
using FTStore.Web.Models;

namespace FTStore.Web.Tests.Fixtures
{
    public class AuthenticationControllerFixture
    {
        public const string EMAIL = "admin@admin.com";
        public const string PASS = "swordfish";
        public const string EMAIL_INVALID = "aadmin@aadmin.com";
        public const string PASS_INVALID = "whale";
        public AuthenticatedUser getAuthenticatedUser()
        {
            return new AuthenticatedUser
            {
                Id = 1,
                Name = Faker.Name.FullName()
            };
        }

        public UserLogin getValidUserLogin()
        {
            return new UserLogin
            {
                Email = EMAIL,
                Password = PASS
            };
        }

        public UserLogin getInvalidUserLogin()
        {
            return new UserLogin
            {
                Email = EMAIL_INVALID,
                Password = PASS_INVALID
            };
        }
    }
}
