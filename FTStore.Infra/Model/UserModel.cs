namespace FTStore.Infra.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public bool IsAdmin { get; set; }
        public int? CustomerId { get; set; }
        public virtual CustomerModel? Customer { get; set; }
    }
}
