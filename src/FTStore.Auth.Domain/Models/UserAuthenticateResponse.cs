using System.Collections.Generic;

namespace FTStore.Auth.Domain.Models
{
    public class UserAuthenticateResponse
    {
        public UserAuthenticateResponse()
        {
            Claims = new List<string>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public List<string> Claims { get; protected set; }
    }
}
