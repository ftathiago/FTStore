using System.IO;

namespace FTStore.App.Repositories
{
    public interface IProductFileManager
    {
        string Save(Stream image, string imageFileName);
        bool Delete(string imageFileName);
    }
}