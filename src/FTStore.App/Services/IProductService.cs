using FTStore.App.Models;
using System.Collections.Generic;
using System.IO;


namespace FTStore.App.Services
{
    public interface IProductService : IServiceBase
    {
        ProductRequest Save(ProductRequest product);
        ProductRequest Update(ProductRequest product);
        bool ReplaceProductImagem(int productId, Stream imageFile, string fileName);
        bool Delete(int id);
        IEnumerable<ProductRequest> ListAll();

        ProductRequest GetById(int id);
    }
}
