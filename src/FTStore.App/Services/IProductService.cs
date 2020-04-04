using FTStore.App.Models;
using System.Collections.Generic;
using System.IO;


namespace FTStore.App.Services
{
    public interface IProductService : IServiceBase
    {
        Product Save(Product product);
        Product Update(Product product);
        bool ReplaceProductImagem(int productId, Stream imageFile, string fileName);
        bool Delete(int id);
        IEnumerable<Product> ListAll();

        Product GetById(int id);
    }
}
