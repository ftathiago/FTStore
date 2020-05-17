using System;
using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using FluentAssertions;

using FTStore.App.Models;
using FTStore.App.Services;
using FTStore.Web.End2End.Test.Fixtures;
using FTStore.Infra.Context;
using FTStore.Web.Controllers;

using Xunit;

namespace FTStore.Web.End2End.Test.Controllers
{
    public class ProductControllerTest : IClassFixture<ServiceCollectionFixture>,
        IClassFixture<DBContextFixture>, IClassFixture<RequestFixture>
    {
        private readonly DBContextFixture _context;
        private readonly ServiceCollectionFixture _serviceCollection;
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestFixture _request;

        public ProductControllerTest(ServiceCollectionFixture serviceCollectionFixture,
            DBContextFixture contextFixture, RequestFixture request)
        {
            _context = contextFixture;
            _serviceCollection = serviceCollectionFixture;
            _serviceProvider = _serviceCollection.ServiceProvider();
            _request = request;
        }

        [Fact]
        public void ShouldReturnNoContentWhenHaveNoProductInDataBase()
        {
            IProductService productService = _serviceProvider.GetService<IProductService>();
            ProductController productController = new ProductController(productService);

            var response = productController.Get();

            Assert.IsType<NoContentResult>(response);
            response.As<NoContentResult>().StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void ShouldReturnOkWhenHaveAnyProduct()
        {
            using var context = _serviceProvider.GetService<FTStoreDbContext>();
            IProductService productService = _serviceProvider.GetService<IProductService>();
            ProductController productController = new ProductController(productService);
            var product = _context.GetValidProduct();
            _context.InsertAProduct(context, product);

            var response = productController.Get();

            Assert.IsType<OkObjectResult>(response);
            response.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void ShouldAddAProduct()
        {
            var product = _request.GetValidProductRequest();
            IProductService productService = _serviceProvider.GetService<IProductService>();
            ProductController productController = new ProductController(productService);

            var response = productController.Post(product);

            Assert.IsType<CreatedResult>(response);
            response.As<CreatedResult>().StatusCode.Should().Be((int)HttpStatusCode.Created);
            using var context = _serviceProvider.GetService<FTStoreDbContext>();
            context.Products.Should().ContainSingle();
        }

        [Fact]
        public void ShouldReturnBadRequestWhenRegisteringProductIsInvalid()
        {
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;
            var productRequest = _request.GetInvalidProductRequest();
            var productService = _serviceProvider.GetService<IProductService>();
            ProductController productController = new ProductController(productService);

            var response = productController.Post(productRequest);

            Assert.IsType<BadRequestObjectResult>(response);
            response.As<BadRequestObjectResult>().StatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public void ShouldBeOkWhenGetProductById()
        {
            const int ID = 1;
            using var context = _serviceProvider.GetService<FTStoreDbContext>();
            var product = _context.GetValidProduct(ID);
            var productExpected = new ProductRequest
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Details,
                Price = product.Price,
                imageFileName = product.ImageFileName
            };
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var productService = _serviceProvider.GetService<IProductService>();
            _context.InsertAProduct(context, product);
            ProductController productController = new ProductController(productService);

            var response = productController.Get(ID);

            Assert.IsType<OkObjectResult>(response);
            response.As<OkObjectResult>().StatusCode.Should().Be(expectedStatusCode);
            response.As<OkObjectResult>().Value.Should().BeEquivalentTo(new { data = productExpected });
        }

        [Fact]
        public void ShouldReturnNoContentWhenProductIdDoesNotExist()
        {
            const int ID = 1;
            var expectedStatusCode = (int)HttpStatusCode.NoContent;
            var productService = _serviceProvider.GetService<IProductService>();
            ProductController productController = new ProductController(productService);

            var response = productController.Get(ID);

            Assert.IsType<NoContentResult>(response);
            response.As<NoContentResult>().StatusCode.Should().Be(expectedStatusCode);
        }
    }
}
