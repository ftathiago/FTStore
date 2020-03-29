using System;
using System.IO;
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
        private const int ID = 1;
        private const int ID_NOTFOUND = 2;
        private readonly IProductFactory _factory;
        private readonly Mock<IProductFileManager> _fileManager;
        private readonly ProductServiceFixture _productServiceFixture;
        private readonly Mock<IProductRepository> _repositoryMock;

        public ProductServiceTest(ProductServiceFixture productServiceFixture)
        {
            _productServiceFixture = productServiceFixture;
            _factory = new ProductFactory();
            _fileManager = new Mock<IProductFileManager>();
            _repositoryMock = new Mock<IProductRepository>();
        }

        [Fact]
        public void ShouldSaveAValidProduct()
        {
            Product product = _productServiceFixture.GetValidProduct();
            _repositoryMock
                .Setup(x => x.Register(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var productReturned = productService.Save(product);

            _repositoryMock.Verify();
            productService.IsValid.Should().BeTrue();
            productReturned.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void ShouldNotSaveAInvalidProduct()
        {
            Product product = _productServiceFixture.GetInvalidProduct();
            _repositoryMock
                .Setup(x => x.Register(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var productReturned = productService.Save(product);

            _repositoryMock.Verify(repo => repo.Register(It.IsAny<ProductEntity>()), Times.Never);
            productService.IsValid.Should().BeFalse();
            productReturned.Should().BeNull();
        }

        [Fact]
        public void ShouldNotSaveIfIDIsNotZero()
        {
            Product product = _productServiceFixture.GetValidProduct(1);
            _repositoryMock
                .Setup(x => x.Register(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var productReturned = productService.Save(product);

            productReturned.Should().BeNull();
            _repositoryMock.Verify(repo => repo.Register(It.IsAny<ProductEntity>()), Times.Never);
            productService.IsValid.Should().BeFalse();
            productService.GetErrorMessages().Should().Contain("The product's Id should not be defined");
        }

        [Fact]
        public void ShouldUpdateProduct()
        {
            Product product = _productServiceFixture.GetValidProduct(ID);
            ProductEntity productEntity = _productServiceFixture.GetValidProductEntity(ID);
            var newPrice = product.Price + 10;
            _repositoryMock
                .Setup(x => x.GetById(ID)).Returns(productEntity)
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Update(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            product.Title = "A modified title";
            product.Details = "A modified, valid and large details to this product";
            product.Price = newPrice;
            var productReturned = productService.Update(product);

            _repositoryMock.Verify();
            productService.IsValid.Should().BeTrue();
            productReturned.Should().BeEquivalentTo(product);
            product.Title.Should().Be(productEntity.Name);
            product.Details.Should().Be(productEntity.Details);
            product.Price.Should().Be(productEntity.Price);
        }

        [Fact]
        public void ShouldNotUpdateInvalidProduct()
        {
            Product product = _productServiceFixture.GetInvalidProduct(ID);
            ProductEntity productEntity = _productServiceFixture.GetValidProductEntity(ID);
            _repositoryMock
                .Setup(x => x.GetById(ID)).Returns(productEntity)
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Update(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var productReturned = productService.Update(product);

            _repositoryMock
                .Verify(x => x.Update(It.IsAny<ProductEntity>()), Times.Never);
            productService.IsValid.Should().BeFalse();
            productReturned.Should().BeNull();
        }

        [Fact]
        public void ShouldUpdateBeInvalidWhenProductNotExists()
        {
            Product product = _productServiceFixture.GetInvalidProduct(ID);
            _repositoryMock
                .Setup(x => x.GetById(ID_NOTFOUND))
                .Returns((ProductEntity)null)
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Update(It.IsAny<ProductEntity>()))
                .Verifiable();
            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var productReturned = productService.Update(product);

            _repositoryMock.Verify(x => x.Update(It.IsAny<ProductEntity>()), Times.Never);
            productReturned.Should().BeNull();
            productService.IsValid.Should().BeFalse();
            productService.GetErrorMessages().Should().Be($"The product [{product.Id} - {product.Title}] was not found");
        }

        [Fact]
        public void ShouldDeleteAProduct()
        {
            ProductEntity productEntity = _productServiceFixture.GetValidProductEntity(ID);
            _repositoryMock
                .Setup(x => x.GetById(ID)).Returns(productEntity)
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Remove(productEntity))
                .Verifiable();
            _fileManager
                .Setup(fm => fm.Delete(productEntity.ImageFileName))
                .Verifiable();

            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var sucessfullyDeleted = productService.Delete(ID);

            _repositoryMock.Verify();
            productService.IsValid.Should().BeTrue();
            sucessfullyDeleted.Should().BeTrue();
        }


        [Fact]
        public void ShouldBeInvalidIfProductDoesExists()
        {
            _repositoryMock
                .Setup(x => x.GetById(ID_NOTFOUND)).Returns((ProductEntity)null)
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Remove(It.IsAny<ProductEntity>()))
                .Verifiable();
            _fileManager
                .Setup(fm => fm.Delete(It.IsAny<string>()))
                .Verifiable();
            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var sucessfullyDeleted = productService.Delete(ID_NOTFOUND);

            _repositoryMock.Verify(x => x.Remove(It.IsAny<ProductEntity>()), Times.Never);
            _fileManager.Verify(x => x.Delete(It.IsAny<string>()), Times.Never);
            productService.IsValid.Should().BeFalse();
            productService.GetErrorMessages().Should().Contain("Product not found");
            sucessfullyDeleted.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeInvalidIfThowsException()
        {
            ProductEntity productEntity = _productServiceFixture.GetValidProductEntity(ID);
            _repositoryMock
                .Setup(x => x.GetById(ID)).Returns(productEntity)
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Remove(productEntity));
            _fileManager
                .Setup(fm => fm.Delete(productEntity.ImageFileName))
                .Throws(new DirectoryNotFoundException("Error message"));

            IProductService productService = new ProductService(
                _repositoryMock.Object,
                _factory,
                _fileManager.Object);

            var sucessfullyDeleted = productService.Delete(ID);

            productService.IsValid.Should().BeFalse();
            productService.GetErrorMessages().Should().Be("Error message");
            sucessfullyDeleted.Should().BeFalse();
        }
    }
}
