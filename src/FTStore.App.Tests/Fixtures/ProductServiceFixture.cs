using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Tests.Fixtures
{
    public class ProductServiceFixture
    {
        public const int ID = 1;
        public const string NAME = "A product name";
        public const string DETAILS = "A large description with more than fifity characters";
        public const string DETAILS_INVALID = "A tinny details";
        public const decimal PRICE = 10M;
        public const decimal PRICE_INVALID = -0.01m;
        public const string IMAGE_FILENAME = "\\teste\\teste";
        public ProductRequest GetValidProduct(int id = 0)
        {
            return new ProductRequest
            {
                Id = id,
                Name = NAME,
                Details = DETAILS,
                Price = PRICE,
                imageFileName = IMAGE_FILENAME
            };
        }

        public ProductRequest GetInvalidProduct(int id = 0)
        {
            return new ProductRequest
            {
                Id = id,
                Name = NAME,
                Details = DETAILS_INVALID,
                Price = PRICE_INVALID,
                imageFileName = IMAGE_FILENAME
            };
        }

        public Product GetValidProductEntity(int id = ID)
        {
            var product = new Product(
                NAME, DETAILS, PRICE, IMAGE_FILENAME);
            product.DefineId(ID);
            return product;
        }
    }
}
