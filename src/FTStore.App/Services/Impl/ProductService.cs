using FTStore.Domain.Repository;
using FTStore.App.Models;
using FTStore.Domain.Entities;
using FTStore.App.Factories;
using System.IO;
using FTStore.App.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public Product Save(Product product)
        {
            if (product.Id != 0)
            {
                AddErrorMessage("The product's Id should not be defined");
                return null;
            }

            ProductEntity productEntity = _productFactory.Convert(product);

            if (!productEntity.IsValid())
            {
                productEntity.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error));
                return null;
            }

            _productRepository.Register(productEntity);
            return (Product)productEntity;
        }

        public Product Update(Product product)
        {
            ProductEntity productEntity = _productRepository.GetById(product.Id);
            if (productEntity == null)
            {
                AddErrorMessage($"The product [{product.Id} - {product.Title}] was not found");
                return null;
            }
            productEntity.ChangeName(product.Title);
            productEntity.ChangeDetails(product.Details);
            productEntity.DefineImageFileName(product.imageFileName);
            productEntity.ChangePrice(product.Price);

            if (!productEntity.IsValid())
            {
                productEntity.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error));
                return null;
            }

            _productRepository.Update(productEntity);
            return product;
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

        public IEnumerable<Product> ListAll()
        {
            return _productRepository.GetAll().Select(p =>
                new Product
                {
                    Id = p.Id,
                    Title = p.Name,
                    Details = p.Details,
                    imageFileName = p.ImageFileName,
                    Price = p.Price
                });
        }

        public bool ReplaceProductImagem(int productId, Stream imageFile, string fileName)
        {
            ProductEntity product = _productRepository.GetById(productId);
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

        private void DeletePreviousFileOf(ProductEntity product)
        {
            var shouldDeletePreviousFile = !string.IsNullOrEmpty(product.ImageFileName);
            if (shouldDeletePreviousFile)
                _productFileManager.Delete(product.ImageFileName);
        }

        private void UpdateProductImageReference(ProductEntity product, string storedFileName)
        {
            product.DefineImageFileName(storedFileName);
            _productRepository.Update(product);
        }
    }
}
