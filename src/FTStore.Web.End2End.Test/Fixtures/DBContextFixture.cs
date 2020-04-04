using FTStore.Infra.Context;
using FTStore.Infra.Model;

namespace FTStore.Web.End2End.Test.Fixtures
{
    public class DBContextFixture
    {
        public void InsertAProduct(FTStoreDbContext context, ProductModel product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public ProductModel GetValidProduct(int id = 0)
        {
            var product = new ProductModel
            {
                Name = "A product title",
                Details = "A large and valid details with more than fifty characters",
                Price = 0.01M,
                ImageFileName = "\\\\the\\path"
            };

            if (id > 0)
                product.Id = id;

            return product;
        }

        public ProductModel GetInvalidProduct(int id = 0)
        {
            var product = new ProductModel
            {
                Name = "A product title",
                Details = "tinny details",
                Price = -0.01M,
                ImageFileName = "\\\\the\\path"
            };

            if (id > 0)
                product.Id = id;

            return product;
        }
    }
}
