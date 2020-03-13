using FTStore.Domain.Repository;
using FTStore.App.Models;
using FTStore.Domain.Entity;
using FTStore.App.Factories;
using System.IO;
using FTStore.App.Repositories;
using System.Collections.Generic;
using System.Linq;

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

        public bool Save(Product product, Stream imageFile, string fileName)
        {
            ProductEntity productEntity = _productFactory.Convert(product);

            productEntity.Validate();
            if (!productEntity.EhValido)
            {
                AddErrorMessage(productEntity.ObterMensagensValidacao());
                return false;
            }

            var storedFileName = _productFileManager.Save(imageFile, fileName);
            productEntity.ImageFileName = storedFileName;
            _productRepository.Adicionar(productEntity);
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
            var produto = _productRepository.ObterPorId(id);
            if (produto == null)
            {
                AddErrorMessage("Product not found");
                return false;
            }

            _productRepository.Remover(produto);
            return true;
        }

        public Product Update(Product product, Stream imagemProduto, string nomeArquivo)
        {
            ProductEntity produtoEntity = _productRepository.ObterPorId(product.Id);
            if (produtoEntity == null)
            {
                AddErrorMessage($"The product {product.Id}-{product.Title} was not found");
                return null;
            }
            produtoEntity.Name = product.Title;
            produtoEntity.Description = product.Details;
            produtoEntity.ImageFileName = product.imageFileName;
            produtoEntity.Price = product.Price;

            if (imagemProduto != null)
            {
                _productFileManager.Delete(product.imageFileName);
                var storedFileName = _productFileManager.Save(imagemProduto, nomeArquivo);
                produtoEntity.ImageFileName = storedFileName;
            }

            produtoEntity.Validate();
            if (!produtoEntity.EhValido)
            {
                AddErrorMessage(produtoEntity.ObterMensagensValidacao());
                return null;
            }

            _productRepository.Atualizar(produtoEntity);
            return product;
        }
    }
}