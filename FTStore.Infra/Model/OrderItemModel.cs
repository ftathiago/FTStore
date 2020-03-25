namespace FTStore.Infra.Model
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}