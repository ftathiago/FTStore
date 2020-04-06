using System.Linq;

using FluentAssertions;

using FTStore.Domain.Entities;
using FTStore.Infra.Repositories;
using FTStore.Infra.Tables;
using FTStore.Infra.Tests.Fixtures;
using FTStore.Infra.Tests.Repositories;

using Xunit;


namespace FTStore.Infra.Tests.Repository
{
    public class ProductRepositoryTest : BaseRepositoryTest<ProductTable>
    {
        private const int PRODUCT_ID = 1;
        private const string PRODUCT_NAME = "A product name";
        private const string DETAILS = "A large description of product's details";
        private const decimal PRICE = 10M;
        private const string IMAGE_FILENAME = "\\\\THE\\PATH";

        public ProductRepositoryTest(ContextFixture contextFixture, AutoMapperFixture autoMapperFixture)
            : base(contextFixture, autoMapperFixture)
        { }

        #region CRUD Operations

        protected override ProductTable GetModelPrototype(int id = 0)
        {
            var product = new ProductTable
            {
                Id = id,
                Name = PRODUCT_NAME,
                Details = DETAILS,
                Price = PRICE,
                ImageFileName = IMAGE_FILENAME
            };
            return product;
        }

        [Fact]
        public override void ShouldRegister()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new ProductRepository(context, MapperFixture.Mapper);
            var product = new Product(PRODUCT_NAME, DETAILS, PRICE, IMAGE_FILENAME);

            repository.Register(product);

            context.Products.Should().ContainSingle();
        }

        [Fact]
        public override void ShouldConserveDataAfterRegister()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new ProductRepository(context, MapperFixture.Mapper);
            var product = new Product(PRODUCT_NAME, DETAILS, PRICE, IMAGE_FILENAME);
            repository.Register(product);

            var productRecovered = context.Products.FirstOrDefault();

            productRecovered.Should().NotBeNull();
            productRecovered.Id.Should().BeGreaterThan(0);
            productRecovered.Name.Should().Be(product.Name);
            productRecovered.Details.Should().Be(product.Details);
            productRecovered.Price.Should().Be(product.Price);
        }

        [Fact]
        public override void ShouldUpdate()
        {
            const string NEW_NAME = "A new name";
            using var context = ContextFixture.Ctx;
            using var repository = new ProductRepository(context, MapperFixture.Mapper);
            AddAtRepository(context, PRODUCT_ID);
            var product = repository.GetById(PRODUCT_ID);

            product.ChangeName(NEW_NAME);
            repository.Update(product);
            var productModified = repository.GetById(PRODUCT_ID);

            context.Products.Should().ContainSingle();
            productModified.Name.Should().Be(NEW_NAME);
        }

        [Fact]
        public override void ShouldDeleteByEntityReference()
        {
            using var context = ContextFixture.Ctx;
            using var repository = new ProductRepository(context, MapperFixture.Mapper);
            AddAtRepository(context, PRODUCT_ID);
            Product product = new Product(PRODUCT_NAME, DETAILS,
                PRICE, IMAGE_FILENAME);
            product.DefineId(PRODUCT_ID);

            repository.Remove(product);

            var productDeleted = !context.Products.Any(p => p.Id == PRODUCT_ID);
            productDeleted.Should().BeTrue();
        }

        #endregion

        #region CRUD invalid temptations

        #endregion
    }
}
