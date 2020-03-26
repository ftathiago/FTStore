using System.Linq;
using FluentAssertions;
using FTStore.Domain.Entities;
using FTStore.Infra.Context;
using FTStore.Infra.Model;
using FTStore.Infra.Repository;
using FTStore.Infra.Tests.Fixture;
using Xunit;

namespace FTStore.Infra.Tests.Repository
{
    public class ProductRepositoryTest : IClassFixture<ContextFixture>, IClassFixture<AutoMapperFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly AutoMapperFixture _mapper;
        private const string PRODUCT_NAME = "A product name";
        private const string DETAILS = "A large description of product's details";
        private const decimal PRICE = 10M;
        private const string IMAGE_FILENAME = "\\\\THE\\PATH";

        public ProductRepositoryTest(ContextFixture contextFixture, AutoMapperFixture autoMapperFixture)
        {
            _contextFixture = contextFixture;
            _mapper = autoMapperFixture;
        }
        #region CRUD Operations

        [Fact]
        public void ShouldRegisterAProduct()
        {
            using var context = _contextFixture.Ctx;
            using var repository = new ProductRepository(context, _mapper.Mapper);
            var product = new ProductEntity(PRODUCT_NAME, DETAILS, PRICE, IMAGE_FILENAME);

            repository.Register(product);

            context.Produtos.Should().ContainSingle();
        }

        [Fact]
        public void ShouldNotLostDataAfterAdd()
        {
            using var context = _contextFixture.Ctx;
            using var repository = new ProductRepository(context, _mapper.Mapper);
            var product = new ProductEntity(PRODUCT_NAME, DETAILS, PRICE, IMAGE_FILENAME);
            repository.Register(product);

            var productRecovered = context.Produtos.FirstOrDefault();

            productRecovered.Should().NotBeNull();
            productRecovered.Id.Should().BeGreaterThan(0);
            productRecovered.Name.Should().Be(product.Name);
            productRecovered.Details.Should().Be(product.Details);
            productRecovered.Price.Should().Be(product.Price);
        }

        [Fact]
        public void ShouldModifyAProductData()
        {
            const int PRODUCT_ID = 1;
            const string NEW_NAME = "A new name";
            using var context = _contextFixture.Ctx;
            using var repository = new ProductRepository(context, _mapper.Mapper);
            BeforeAddAProductAtRepository(context, PRODUCT_ID);
            var product = repository.GetById(PRODUCT_ID);

            product.ChangeName(NEW_NAME);
            repository.Update(product);
            var productModified = repository.GetById(PRODUCT_ID);

            context.Produtos.Should().ContainSingle();
            productModified.Name.Should().Be(NEW_NAME);
        }
        #endregion

        #region CRUD invalid temptations

        #endregion

        private void BeforeAddAProductAtRepository(FTStoreDbContext context, int id = 0)
        {
            var product = new ProductModel
            {
                Id = id,
                Name = PRODUCT_NAME,
                Details = DETAILS,
                Price = PRICE,
                ImageFileName = IMAGE_FILENAME
            };
            context.Produtos.Add(product);
            context.SaveChanges();
        }
    }
}
