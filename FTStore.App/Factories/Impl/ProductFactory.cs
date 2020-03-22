using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories.Impl
{
    public class ProductFactory : IProductFactory
    {
        public ProductEntity Convert(Product product)
        {
            ProductEntity productEntity = new ProductEntity
            {
                Description = product.Details,
                Id = product.Id > 0 ? product.Id : 0,
                Name = product.Title,
                Price = product.Price,
                ImageFileName = product.imageFileName
            };
            return productEntity;
        }
    }
}
