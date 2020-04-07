using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using FTStore.App.Factories;
using FTStore.App.Models;
using FTStore.App.Repositories;
using FTStore.Domain.Entities;
using FTStore.Domain.Repositories;
using FTStore.Lib.Common.Services;


namespace FTStore.App.Services.Impl
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFactory _productFactory;

        public IProductFileManager _productFileManager { get; }

        public ProductService(
            IProductRepository productRepository,
            IProductFactory productFactory,
            IProductFileManager productFileManager)
        {
            _productRepository = productRepository;
            _productFactory = productFactory;
            _productFileManager = productFileManager;
        }

        public ProductRequest Save(ProductRequest productRequest)
        {
            if (productRequest.Id != 0)
            {
                AddErrorMessage("The product's Id should not be defined");
                return null;
            }

            Product product = _productFactory.Convert(productRequest);

            if (!product.IsValid())
            {
                product.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error));
                return null;
            }

            _productRepository.Register(product);
            return (ProductRequest)product;
        }

        public ProductRequest Update(ProductRequest productRequest)
        {
            Product product = _productRepository.GetById(productRequest.Id);
            if (product == null)
            {
                AddErrorMessage($"The product [{productRequest.Id} - {productRequest.Name}] was not found");
                return null;
            }
            product.ChangeName(productRequest.Name);
            product.ChangeDetails(productRequest.Details);
            product.DefineImageFileName(productRequest.imageFileName);
            product.ChangePrice(productRequest.Price);

            if (!product.IsValid())
            {
                product.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error));
                return null;
            }

            _productRepository.Update(product);
            return productRequest;
        }

        public bool Delete(int id)
        {
            var result = false;
            try
            {
                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    AddErrorMessage("Product not found");
                    return false;
                }
                var productImagemFileName = product.ImageFileName;
                _productRepository.Remove(product);
                _productFileManager.Delete(productImagemFileName);
                result = true;
            }
            catch (IOException e)
            {
                ExceptionHandler(e);
            }
            return result;
        }

        public IEnumerable<ProductRequest> ListAll()
        {
            return _productRepository.GetAll().Select(p =>
                new ProductRequest
                {
                    Id = p.Id,
                    Name = p.Name,
                    Details = p.Details,
                    imageFileName = p.ImageFileName,
                    Price = p.Price
                });
        }

        public ProductRequest GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return null;
            return new ProductRequest
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Details,
                imageFileName = product.ImageFileName,
                Price = product.Price
            };
        }

        public bool ReplaceProductImagem(int productId, Stream imageFile, string fileName)
        {
            Product product = _productRepository.GetById(productId);
            if (product == null)
            {
                AddErrorMessage($"Product {productId} not found");
                return false;
            }
            try
            {
                var storedFileName = SaveFile(imageFile, fileName);
                DeletePreviousFileOf(product);
                UpdateProductImageReference(product, storedFileName);
                return true;
            }
            catch (Exception exception)
            {
                ExceptionHandler(exception);
                return false;
            }
        }

        private string SaveFile(Stream file, string filename)
        {
            if (file == null || file.Length == 0)
                return string.Empty;
            if (string.IsNullOrEmpty(filename))
                return string.Empty;
            return _productFileManager.Save(file, filename);
        }

        private void DeletePreviousFileOf(Product product)
        {
            var shouldDeletePreviousFile = !string.IsNullOrEmpty(product.ImageFileName);
            if (shouldDeletePreviousFile)
                _productFileManager.Delete(product.ImageFileName);
        }

        private void UpdateProductImageReference(Product product, string storedFileName)
        {
            product.DefineImageFileName(storedFileName);
            _productRepository.Update(product);
        }
    }
}
