using System;
using System.IO;
using System.Linq;
using FTStore.App.Repositories;

namespace FTStore.Infra.Resource
{
    public class ProductFileManager : IProductFileManager
    {
        private readonly string _baseDir;
        private const string PRODUCTS_IMAGE_DIR = "\\assets\\images\\produto\\";

        public ProductFileManager(string baseDir)
        {
            _baseDir = baseDir;
        }
        public void Delete(string imageFileName)
        {
            var fullPath = _baseDir + PRODUCTS_IMAGE_DIR + imageFileName;
            if (!File.Exists(fullPath))
                return;
            File.Delete(fullPath);
        }

        public string Save(Stream image, string imageFileName)
        {
            if (image == null)
                return string.Empty;

            var fileName = GenerateFileNameBasedOn(imageFileName);
            var path = _baseDir + PRODUCTS_IMAGE_DIR;
            var fullPath = path + fileName;
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                image.Position = 0;
                image.CopyTo(fileStream);
            }
            return fileName;
        }

        private string GenerateFileNameBasedOn(string fileName)
        {
            var extension = fileName.Split(".").Last();
            var fileNameWithoutExtension = Path
                .GetFileNameWithoutExtension(fileName)
                .Take(10);
            var newFileName = new string(fileNameWithoutExtension.ToArray()).Replace(" ", "-") + "." + extension;
            newFileName = $"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{newFileName}";
            return newFileName;
        }
    }
}
