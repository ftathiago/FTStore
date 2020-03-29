using FluentAssertions;
using FTStore.App.Factories;
using FTStore.App.Factories.Impl;
using FTStore.App.Models;
using FTStore.App.Repositories;
using FTStore.App.Services;
using FTStore.App.Services.Impl;
using FTStore.App.Tests.Fixtures;
using FTStore.Domain.Entities;
using FTStore.Domain.Repository;
using Moq;
using Xunit;

namespace FTStore.App.Tests.Services
{
    public class ProductServiceTest : IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _productServiceFixture;

        public ProductServiceTest(ProductServiceFixture productServiceFixture)
        {
            _productServiceFixture = productServiceFixture;
        }

        [Fact]
        public void ShouldSaveAValidProduct()
        {
            Product product = _productServiceFixture.GetValidProduct();
            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock
                .Setup(x => x.Register(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductFactory factory = new ProductFactory();
            IProductFileManager fileManager = new Mock<IProductFileManager>().Object;
            IProductService productService = new ProductService(
                repositoryMock.Object,
                factory,
                fileManager);


            var productReturned = productService.Save(product);

            repositoryMock.Verify();
            productService.IsValid.Should().BeTrue();
            productReturned.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void ShouldNotSaveAInvalidProduct()
        {
            Product product = _productServiceFixture.GetInvalidProduct();
            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock
                .Setup(x => x.Register(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductFactory factory = new ProductFactory();
            IProductFileManager fileManager = new Mock<IProductFileManager>().Object;
            IProductService productService = new ProductService(
                repositoryMock.Object,
                factory,
                fileManager);


            var productReturned = productService.Save(product);

            repositoryMock.Verify(repo => repo.Register(It.IsAny<ProductEntity>()), Times.Never);
            productService.IsValid.Should().BeFalse();
            productReturned.Should().BeNull();
        }

        [Fact]
        public void ShouldNotSaveIfIDIsNotZero()
        {
            Product product = _productServiceFixture.GetValidProduct(1);

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock
                .Setup(x => x.Register(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductFactory factory = new ProductFactory();
            IProductFileManager fileManager = new Mock<IProductFileManager>().Object;
            IProductService productService = new ProductService(
                repositoryMock.Object,
                factory,
                fileManager);

            var productReturned = productService.Save(product);

            repositoryMock.Verify(repo => repo.Register(It.IsAny<ProductEntity>()), Times.Never);
            productService.IsValid.Should().BeFalse();
            productReturned.Should().BeNull();
        }
    }
}
