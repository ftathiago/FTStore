using System;
using FTStore.Domain.Entity;

namespace FTStore.App.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string imageFileName { get; set; }

        public static explicit operator Product(ProductEntity productEntity)
        {
            if (productEntity == null)
                return null;
            return new Product
            {
                Id = productEntity.Id,
                Title = productEntity.Name,
                Details = productEntity.Description,
                Price = productEntity.Price,
                imageFileName = productEntity.ImageFileName
            };
        }
    }
}
