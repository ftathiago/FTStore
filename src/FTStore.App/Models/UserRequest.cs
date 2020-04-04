namespace FTStore.App.Models
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public bool IsAdmin { get; set; }
    }
}
