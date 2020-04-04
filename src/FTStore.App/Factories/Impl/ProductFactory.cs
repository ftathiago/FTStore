using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories.Impl
{
    public class ProductFactory : IProductFactory
    {
        public ProductEntity Convert(ProductRequest product)
        {
            ProductEntity productEntity = new ProductEntity(
                product.Name,
                product.Details,
                product.Price,
                product.imageFileName
            );
            productEntity.DefineId(product.Id);
            return productEntity;
        }
    }
}
