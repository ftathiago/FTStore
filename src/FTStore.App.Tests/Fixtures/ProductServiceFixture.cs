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
        public Product GetValidProduct(int id = 0)
        {
            return new Product
            {
                Id = id,
                Title = NAME,
                Details = DETAILS,
                Price = PRICE,
                imageFileName = IMAGE_FILENAME
            };
        }

        public Product GetInvalidProduct(int id = 0)
        {
            return new Product
            {
                Id = id,
                Title = NAME,
                Details = DETAILS_INVALID,
                Price = PRICE_INVALID,
                imageFileName = IMAGE_FILENAME
            };
        }

        public ProductEntity GetValidProductEntity(int id = ID)
        {
            var product = new ProductEntity(
                NAME, DETAILS, PRICE, IMAGE_FILENAME);
            product.DefineId(ID);
            return product;
        }
    }
}
