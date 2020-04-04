using FTStore.Domain.Entities;
using FTStore.Infra.Table;

namespace FTStore.App.Tests.Fixture.Repository
{
    public class ProductFixture
    {
        public const int ID = 1;
        public const string NAME = "A product name";
        public const string DETAILS = "A large product details with more than 50 characters";
        public const string IMAGE_FILENAME = "\\\\THE\\PATH";
        public const decimal PRICE = 10;
        public ProductTable GetValid(int id = ID)
        {
            var product = new ProductTable
            {
                Id = id,
                Name = NAME,
                Details = DETAILS,
                ImageFileName = IMAGE_FILENAME,
                Price = PRICE
            };
            return product;
        }

        public Product GetValidEntity(int id = ID)
        {
            var product = new Product(
                ProductFixture.NAME,
                ProductFixture.DETAILS,
                ProductFixture.PRICE,
                ProductFixture.IMAGE_FILENAME
            );
            product.DefineId(id);
            return product;
        }
    }
}
