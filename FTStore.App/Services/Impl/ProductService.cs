using FTStore.Domain.Repository;
using FTStore.App.Models;
using FTStore.Domain.Entity;
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

        public ProductService(IProductRepository productRepository,
            IProductFactory productFactory,
            IProductFileManager productFileManager)
        {
            _productRepository = productRepository;
            _productFactory = productFactory;
            _productFileManager = productFileManager;
        }

        public Product Save(Product product)
        {
            ProductEntity productEntity = _productFactory.Convert(product);

            productEntity.Validate();
            if (!productEntity.EhValido)
            {
                AddErrorMessage(productEntity.ObterMensagensValidacao());
                return null;
            }

            _productRepository.Adicionar(productEntity);
            return (Product)productEntity;
        }
        public bool AddProductImage(int productId, Stream imageFile, string fileName)
        {
            ProductEntity product = _productRepository.ObterPorId(productId);
            if (product == null)
            {
                AddErrorMessage($"Product {productId} not found");
                return false;
            }
            try
            {
                var storedFileName = _productFileManager.Save(imageFile, fileName);
                DeletePreviouFileOf(product);
                UpdateProductImageReference(product, storedFileName);
                return true;
            }
            catch (Exception exception)
            {
                ExceptionHandler(exception);
                return false;
            }
        }

        private void DeletePreviouFileOf(ProductEntity product)
        {
            var shouldDeletePreviousFile = !string.IsNullOrEmpty(product.ImageFileName);
            if (shouldDeletePreviousFile)
                _productFileManager.Delete(product.ImageFileName);
        }

        private bool UpdateProductImageReference(ProductEntity product, string storedFileName)
        {
            if (string.IsNullOrEmpty(storedFileName))
                return false;
            product.ImageFileName = storedFileName;
            _productRepository.Atualizar(product);
            return true;
        }

        public IEnumerable<Product> ListAll()
        {
            return _productRepository.ObterTodos().Select(p =>
                new Product
                {
                    Id = p.Id,
                    Title = p.Name,
                    Details = p.Description,
                    imageFileName = p.ImageFileName,
                    Price = p.Price
                });
        }

        public bool Delete(int id)
        {
            var product = _productRepository.ObterPorId(id);
            if (product == null)
            {
                AddErrorMessage("Product not found");
                return false;
            }
            var productImagemFileName = product.ImageFileName;
            _productRepository.Remover(product);
            _productFileManager.Delete(productImagemFileName);
            return true;
        }

        public Product Update(Product product)
        {
            ProductEntity productEntity = _productRepository.ObterPorId(product.Id);
            if (productEntity == null)
            {
                AddErrorMessage($"The product {product.Id}-{product.Title} was not found");
                return null;
            }
            productEntity.Name = product.Title;
            productEntity.Description = product.Details;
            productEntity.ImageFileName = product.imageFileName;
            productEntity.Price = product.Price;

            productEntity.Validate();
            if (!productEntity.EhValido)
            {
                AddErrorMessage(productEntity.ObterMensagensValidacao());
                return null;
            }

            _productRepository.Atualizar(productEntity);
            return product;
        }
    }
}
