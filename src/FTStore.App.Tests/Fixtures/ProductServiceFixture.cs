using FTStore.App.Models;

namespace FTStore.App.Tests.Fixtures
{
    public class ProductServiceFixture
    {
        public Product GetValidProduct(int id = 0)
        {
            return new Product
            {
                Id = id,
                Title = "A product title",
                Details = "A large description with more than fifty characters",
                Price = 10,
                imageFileName = "\\teste\\teste"
            };
        }

        public Product GetInvalidProduct(int id = 0)
        {
            return new Product
            {
                Id = id,
                Title = "A product title",
                Details = "A tinny description",
                Price = -1,
                imageFileName = "\\teste\\teste"
            };
        }
    }
}
