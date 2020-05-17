using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using FluentAssertions;

using FTStore.App.Models;
using FTStore.App.Services;
using FTStore.Web.Controllers;
using FTStore.Web.Tests.Fixtures;

using Moq;

using Xunit;

namespace FTStore.Web.Tests.Controllers
{
    public class ProductControllerTest : IClassFixture<ProductControllerFixture>
    {
        private readonly ProductControllerFixture _productControllerFixture;

        public ProductControllerTest(ProductControllerFixture productControllerFixture)
        {
            _productControllerFixture = productControllerFixture;
        }

        [Fact]
        public void ShouldPostReturnBadRequestWhenProductIsNull()
        {
            var statusCodeExpected = (int)HttpStatusCode.BadRequest;
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            ProductController productController = new ProductController(
                productService.Object);

            var response = productController.Post(null);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<BadRequestObjectResult>().Value.Should().Be("There is no product information to handle");
        }

        [Fact]
        public void ShouldAddProduct()
        {
            var product = new ProductRequest
            {
                Id = 0,
                Name = "Title",
                Details = "Details",
                imageFileName = "imageFileName",
                Price = 10M
            };
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(x => x.Save(product))
                .Returns(product);
            ProductController productController = new ProductController(
                productService.Object);
            var statusCodeExpected = (int)HttpStatusCode.Created;

            var response = productController.Post(product);

            response.As<CreatedResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<CreatedResult>().Value.Should().Be(product);
        }

        [Fact]
        public void ShouldReturnBadRequestWhenCanNotSaveProduct()
        {
            const string ERROR_WHILE_SAVING = "ERROR WHILE SAVING";
            var product = new ProductRequest
            {
                Id = 0,
                Name = "Title",
                Details = "Details",
                imageFileName = "imageFileName",
                Price = 10M
            };
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(x => x.Save(product))
                .Returns((ProductRequest)null);
            productService
                .Setup(x => x.GetErrorMessages())
                .Returns(ERROR_WHILE_SAVING);
            ProductController productController = new ProductController(
                productService.Object);
            var statusCodeExpected = (int)HttpStatusCode.BadRequest;

            var response = productController.Post(product);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<BadRequestObjectResult>().Value.Should().Be(ERROR_WHILE_SAVING);
        }

        [Fact]
        public void ShouldUploadProductImage()
        {
            const int ID = 1;
            const string FILE_NAME = "file.txt";
            var statusCodeExpected = (int)HttpStatusCode.OK;
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(x => x.ReplaceProductImagem(ID, It.IsAny<Stream>(), FILE_NAME))
                .Returns(true);
            ProductController productController = new ProductController(
                productService.Object);
            productController.ControllerContext = _productControllerFixture.RequestWithFile(FILE_NAME);

            var response = productController.UploadProductImagem(ID);

            response.As<OkResult>().StatusCode.Should().Be(statusCodeExpected);
        }

        [Fact]
        public void ShouldReturnBadRequestWithoutProductImage()
        {
            const int ID = 1;
            var statusCodeExpected = (int)HttpStatusCode.BadRequest;
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            ProductController productController = new ProductController(
                productService.Object);
            productController.ControllerContext = _productControllerFixture.RequestWithoutFile("");

            var response = productController.UploadProductImagem(ID);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(statusCodeExpected);
        }

        [Fact]
        public void ShouldReturnBadRequestWhenFileNotUploaded()
        {
            const int ID = 1;
            const string FILE_NAME = "file.txt";
            var statusCodeExpected = (int)HttpStatusCode.BadRequest;
            var productService = new Mock<IProductService>();
            productService
                .Setup(x => x.ReplaceProductImagem(ID, It.IsAny<Stream>(), FILE_NAME))
                .Returns(false);
            ProductController productController = new ProductController(
                productService.Object);
            productController.ControllerContext = _productControllerFixture.RequestWithFile(FILE_NAME);

            var response = productController.UploadProductImagem(ID);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(statusCodeExpected);
        }

        [Fact]
        public void ShouldReturnOkWhenProductDeleted()
        {
            const int ID = 1;
            var statusCodeExpected = (int)HttpStatusCode.OK;
            var productService = new Mock<IProductService>();
            productService
                .Setup(x => x.Delete(ID))
                .Returns(true);
            ProductController productController = new ProductController(
                productService.Object);

            var response = productController.Delete(ID);

            response.As<OkResult>().StatusCode.Should().Be(statusCodeExpected);
        }

        [Fact]
        public void ShouldReturnBadRequestWhenProductCanNotBeDeleted()
        {
            const int ID_NOTFOUND = 1;
            var statusCodeExpected = (int)HttpStatusCode.BadRequest;
            var productService = new Mock<IProductService>();
            productService
                .Setup(x => x.Delete(ID_NOTFOUND))
                .Returns(false);
            ProductController productController = new ProductController(
                productService.Object);

            var response = productController.Delete(ID_NOTFOUND);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(statusCodeExpected);
        }

        [Fact]
        public void ShouldModifyProduct()
        {
            var statusCodeExpected = (int)HttpStatusCode.OK;
            var product = new ProductRequest();
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(ps => ps.Update(product))
                .Returns(product)
                .Verifiable();
            ProductController productController = new ProductController(productService.Object);

            var response = productController.Put(product);

            response.As<OkObjectResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<OkObjectResult>().Value.Should().Be(product);
            productService.Verify();
        }

        [Fact]
        public void ShouldReturnBadRequestWhenProductIsNull()
        {
            var statusCodeExpected = (int)HttpStatusCode.BadRequest;
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            var productController = new ProductController(productService.Object);

            var response = productController.Put(null);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<BadRequestObjectResult>().Value.Should().Be("There is no product information to handle");
            productService.Verify();
        }

        [Fact]
        public void ShouldReturnNullProductWhenHaveAUpdateError()
        {
            const string ERROR_MESSAGE = "Error message";
            var statusCodeExpected = (int)HttpStatusCode.BadRequest;
            var product = new ProductRequest();
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(ps => ps.Update(product))
                .Returns((ProductRequest)null)
                .Verifiable();
            productService
                .Setup(ps => ps.GetErrorMessages())
                .Returns(ERROR_MESSAGE);
            var productController = new ProductController(productService.Object);

            var response = productController.Put(product);

            response.As<BadRequestObjectResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<BadRequestObjectResult>().Value.Should().Be(ERROR_MESSAGE);
            productService.Verify();
        }

        [Fact]
        public void ShouldReturnBadRequestWhenExceptionOccurs()
        {
            const string EXCEPTION_MESSAGE = "Exception Message";
            var statusCodeExpected = (int)HttpStatusCode.InternalServerError;
            var product = new ProductRequest();
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(ps => ps.Update(It.IsAny<ProductRequest>()))
                .Throws(new Exception(EXCEPTION_MESSAGE));
            var productController = new ProductController(productService.Object);

            var response = productController.Put(product);

            response.As<ObjectResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<ObjectResult>().Value.Should().BeEquivalentTo(new { error = EXCEPTION_MESSAGE });
        }

        [Fact]
        public void ShouldReturnAListOfProducts()
        {
            var productList = new List<ProductRequest>
            {
                new ProductRequest
                {
                    Id  = 1,
                    Name = "Title"
                },
                new ProductRequest
                {
                    Id = 2,
                    Name = "Title 2"
                }
            };
            var statusCodeExpected = (int)HttpStatusCode.OK;
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(ps => ps.ListAll())
                .Returns(productList)
                .Verifiable();
            var productController = new ProductController(productService.Object);

            var response = productController.Get();

            response.As<OkObjectResult>().StatusCode.Should().Be(statusCodeExpected);
            response.As<OkObjectResult>().Value.Should().BeEquivalentTo(new { data = productList });
            productService.Verify();
        }

        [Fact]
        public void ShouldReturnNoContentWhenThereIsNoProductToList()
        {
            var productList = new List<ProductRequest>();
            var statusCodeExpected = (int)HttpStatusCode.NoContent;
            var productService = new Mock<IProductService>(MockBehavior.Strict);
            productService
                .Setup(ps => ps.ListAll())
                .Returns(productList)
                .Verifiable();
            var productController = new ProductController(productService.Object);

            var response = productController.Get();

            response.As<NoContentResult>().StatusCode.Should().Be(statusCodeExpected);
            productService.Verify();
        }
    }
}
