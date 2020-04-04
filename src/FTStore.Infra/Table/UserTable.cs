namespace FTStore.Infra.Table
{
    public class UserTable
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public bool IsAdmin { get; set; }
        public int? CustomerId { get; set; }
        public virtual CustomerTable Customer { get; set; }
    }
}
