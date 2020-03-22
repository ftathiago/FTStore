namespace FTStore.Domain.Entity
{
    public class ProductEntity : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageFileName { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                AdicionarCritica("Product name is required");
            if (string.IsNullOrEmpty(Description))
                AdicionarCritica("Product descript is required");
            if (Price <= 0)
                AdicionarCritica("Price must be over than zero");
        }
    }
}