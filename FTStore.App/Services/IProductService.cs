using FTStore.App.Models;
using System.Collections.Generic;
using System.IO;


namespace FTStore.App.Services
{
    public interface IProductService : IServiceBase
    {
        bool Save(Product product, Stream imageFile, string fileName);
        Product Update(Product product, Stream imageFile, string fileName);
        bool Delete(int id);
        IEnumerable<Product> ListAll();
    }
}