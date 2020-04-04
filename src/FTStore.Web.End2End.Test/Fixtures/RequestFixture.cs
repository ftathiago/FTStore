using FTStore.App.Models;

namespace FTStore.Web.End2End.Test.Fixtures
{
    public class RequestFixture
    {
        public ProductRequest GetValidProductRequest(int id = 0)
        {
            var product = new ProductRequest
            {
                Name = "A product name",
                Details = "A large and valid detail with more than fifity characters",
                Price = 0.01M,
                imageFileName = "\\\\the\\path"
            };
            if (id > 0)
                product.Id = id;
            return product;
        }

        public ProductRequest GetInvalidProductRequest(int id = 0)
        {
            var product = new ProductRequest
            {
                Name = "A product name",
                Details = "Tiny details",
                Price = -0.01M,
                imageFileName = "\\\\the\\path"
            };
            if (id > 0)
                product.Id = id;
            return product;
        }
    }
}
