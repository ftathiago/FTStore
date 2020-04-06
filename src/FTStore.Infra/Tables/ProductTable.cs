namespace FTStore.Infra.Tables
{
    public class ProductTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string ImageFileName { get; set; }
    }
}
