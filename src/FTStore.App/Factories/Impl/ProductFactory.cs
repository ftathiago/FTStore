using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories.Impl
{
    public class ProductFactory : IProductFactory
    {
        public Product Convert(ProductRequest product)
        {
            Product productEntity = new Product(
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
