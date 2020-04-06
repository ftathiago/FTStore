using FTStore.Infra.Context;
using FTStore.Infra.Tables;

namespace FTStore.Web.End2End.Test.Fixtures
{
    public class DBContextFixture
    {
        public void InsertAProduct(FTStoreDbContext context, ProductTable product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public ProductTable GetValidProduct(int id = 0)
        {
            var product = new ProductTable
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

        public ProductTable GetInvalidProduct(int id = 0)
        {
            var product = new ProductTable
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
