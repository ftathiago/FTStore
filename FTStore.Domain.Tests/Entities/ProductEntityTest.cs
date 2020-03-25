using FluentAssertions;
using FTStore.Domain.Entities;
using Xunit;
using System.Linq;
using FTStore.Domain.Tests.Entities.Prototypes;

namespace FTStore.Domain.Tests.Entities
{
    public class ProductEntityTest
    {

        [Fact]
        public void ShouldCreateAProduct()
        {
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            product.Should().NotBeNull();
        }

        [Fact]
        public void ShouldChangeTitle()
        {
            const string NEW_TITLE = "A new title";
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            product.ChangeTitle(NEW_TITLE);

            product.Title.Should().BeSameAs(NEW_TITLE);
        }

        [Fact]
        public void ShouldChangeDetails()
        {
            const string NEW_DETAILS = "A new detail";
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            product.ChangeDetails(NEW_DETAILS);

            product.Details.Should().BeSameAs(NEW_DETAILS);
        }

        [Fact]
        public void ShouldChangePrice()
        {
            const decimal NEW_PRICE = 666.66M;
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            product.ChangePrice(NEW_PRICE);

            (product.Price == NEW_PRICE).Should().BeTrue();
        }

        [Fact]
        public void ImageFileNameShouldBeSetted()
        {
            const string NEW_FILENAME = "\\DEFINE\\ANEW\\PATH";
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            product.DefineImageFileName(NEW_FILENAME);

            product.ImageFileName.Should().BeSameAs(NEW_FILENAME);
        }

        [Fact]
        public void ShouldValidateEmptyTitle()
        {
            var product = new ProductEntity(title: string.Empty,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            var isValid = product.IsValid();

            isValid.Should().BeFalse();
            product.ValidationResult.Errors.Should().ContainSingle();
        }

        [Fact]
        public void ShouldValidateEmptyTitleWithMessage()
        {
            const string EXPECTED_ERROR_MESSAGE = "The product's title is required";
            var product = new ProductEntity(title: string.Empty,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);
            product.IsValid();

            var errorsFound = product.ValidationResult.Errors;

            errorsFound.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void DescriptionShouldHave50CharAtLeast()
        {
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.INVALID_DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            var isValid = product.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void DescriptionWithLessThan50CharHasMessage()
        {
            const string EXPECTED_ERROR_MESSAGE = "The product's description must have 50 characters at least";
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.INVALID_DESCRIPTION,
                ProductEntityPrototype.PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);
            product.IsValid();

            var errorsFound = product.ValidationResult.Errors;

            errorsFound.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }

        [Fact]
        public void ProductPriceMustBeGreaterThanZero()
        {
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.INVALID_PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);

            var isValid = product.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void PriceMustBeGreaterThanZero()
        {
            const string EXPECTED_ERROR_MESSAGE = "The product's prices must be greater than zero";
            var product = new ProductEntity(
                ProductEntityPrototype.TITLE,
                ProductEntityPrototype.DESCRIPTION,
                ProductEntityPrototype.INVALID_PRICE,
                ProductEntityPrototype.IMAGE_FILENAME);
            product.IsValid();

            var errorsFound = product.ValidationResult.Errors;

            errorsFound.Should().Contain(error => error.ErrorMessage == EXPECTED_ERROR_MESSAGE);
        }
    }
}
