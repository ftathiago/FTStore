using FTStore.Domain.Entities;

namespace FTStore.App.Models
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string imageFileName { get; set; }

        public static explicit operator ProductRequest(ProductEntity productEntity)
        {
            if (productEntity == null)
                return null;
            return new ProductRequest
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Details = productEntity.Details,
                Price = productEntity.Price,
                imageFileName = productEntity.ImageFileName
            };
        }
    }
}
