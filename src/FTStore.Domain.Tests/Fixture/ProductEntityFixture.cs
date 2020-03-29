using FTStore.Domain.Entities;

namespace FTStore.Domain.Tests.Fixture
{
    internal static class ProductEntityFixture
    {
        public const string TITLE = "A Product title";
        public const string DESCRIPTION = "A large description with more than fifity characters";
        public const string INVALID_DESCRIPTION = "A tiny descriptions";
        public const decimal PRICE = 1.01M;
        public const decimal INVALID_PRICE = 0;
        public const string IMAGE_FILENAME = "C:\\FILENAME.jpg";

        public static ProductEntity GetValidProduct(int productId = 0)
        {
            var product = new ProductEntity(TITLE, DESCRIPTION, PRICE,
                IMAGE_FILENAME);
            product.DefineId(productId);
            return product;
        }

        public static ProductEntity GetInvalidProduct(int productId = 0)
        {
            var product = new ProductEntity(
                TITLE,
                INVALID_DESCRIPTION,
                INVALID_PRICE,
                IMAGE_FILENAME);
            product.DefineId(productId);
            return product;
        }
    }
}
